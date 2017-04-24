using NUnit.Framework;
using Opten.Common.Helpers;
using System;

namespace Opten.Common.Test.Helpers
{
	[TestFixture]
	public class FileTests
	{

		[Test]
		public void Get_FileName_With_Date()
		{
			DateTime dt = new DateTime(2015, 1, 10);

			Assert.AreEqual("test_10012015.png", FileHelper.GetFileNameWithDate(fileName: "test.png", date: dt, format: "ddMMyyyy"));
			Assert.AreEqual("test_10012015.png", FileHelper.GetFileNameWithDate(fileName: "TEST.png", date: dt, format: "ddMMyyyy"));
			Assert.AreEqual("test_10012015.mp4", FileHelper.GetFileNameWithDate(fileName: "test.MP4", date: dt, format: "ddMMyyyy"));
			Assert.AreEqual("test_test2_10012015.mp4", FileHelper.GetFileNameWithDate(fileName: "test test2 .MP4", date: dt, format: "ddMMyyyy"));
		}

		[Test]
		public void Get_Ugly_FileName()
		{
			Assert.AreEqual("file-1992-buero", FileHelper.RemoveInvalidCharactersFromFileName(fileName: "file-1992-büro"));
			Assert.AreEqual("file$$$()", FileHelper.RemoveInvalidCharactersFromFileName(fileName: "file$$$()??"));
			Assert.AreEqual("schoene_sommerferien", FileHelper.RemoveInvalidCharactersFromFileName(fileName: "schöne_sommerferien"));
			Assert.AreEqual("test_abc_def", FileHelper.RemoveInvalidCharactersFromFileName(fileName: "test_abc_def"));
			Assert.AreEqual("test", FileHelper.RemoveInvalidCharactersFromFileName(fileName: "test_"));
			Assert.AreEqual("test_test2", FileHelper.RemoveInvalidCharactersFromFileName(fileName: "test test2 "));
			Assert.AreEqual("test_test2", FileHelper.RemoveInvalidCharactersFromFileName(fileName: "test                  test2 "));
		}

		[Test]
		public void Get_FileName_With_Extension()
		{
			Assert.AreEqual("file.png", FileHelper.GetFileNameWithExtension(fileName: "file", extension: "png"));
			Assert.AreEqual("file.png", FileHelper.GetFileNameWithExtension(fileName: "file.png", extension: ".png"));
			Assert.AreEqual("file.png", FileHelper.GetFileNameWithExtension(fileName: "file.jpg", extension: "png"));
		}

		[Test]
		public void Get_File_Name_Without_Extension()
		{
			Assert.AreEqual("file", FileHelper.GetFileNameWithoutExtension(fileName: "file"));
			Assert.AreEqual("file", FileHelper.GetFileNameWithoutExtension(fileName: "file.png"));

			Assert.AreEqual("file.test1", FileHelper.GetFileNameWithoutExtension(fileName: "file.test1.test2"));
			Assert.AreEqual("file.test1.test2", FileHelper.GetFileNameWithoutExtension(fileName: "file.test1.test2.png"));
		}

		[Test]
		public void Get_Extension_By_FileName()
		{
			Assert.AreEqual("png", FileHelper.GetExtensionByFileName(fileName: "file.png"));
			Assert.AreEqual("png", FileHelper.GetExtensionByFileName(fileName: "file.test1.test2.png"));

			Assert.Throws<NotSupportedException>(() => FileHelper.GetExtensionByFileName(fileName: "file"));
		}

		[Test]
		public void Get_Content_Type()
		{
			Assert.AreEqual(System.Net.Mime.MediaTypeNames.Image.Jpeg, FileHelper.GetContentType(fileNameOrExtension: "file.jpg"));
			Assert.AreEqual(System.Net.Mime.MediaTypeNames.Image.Jpeg, FileHelper.GetContentType(fileNameOrExtension: "file.test1.test2.jpe"));
			Assert.AreEqual(System.Net.Mime.MediaTypeNames.Image.Jpeg, FileHelper.GetContentType(fileNameOrExtension: "jpg"));
			Assert.AreEqual(System.Net.Mime.MediaTypeNames.Image.Jpeg, FileHelper.GetContentType(fileNameOrExtension: ".jpeg"));
			Assert.AreEqual(System.Net.Mime.MediaTypeNames.Application.Octet, FileHelper.GetContentType(fileNameOrExtension: "notExistingExtension"));
			Assert.AreEqual(System.Net.Mime.MediaTypeNames.Application.Octet, FileHelper.GetContentType(fileNameOrExtension: "not.ExistingExtension"));

			Assert.Throws<ArgumentNullException>(() => FileHelper.GetContentType(fileNameOrExtension: null));
		}

	}
}