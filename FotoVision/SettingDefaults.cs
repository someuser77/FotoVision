using System;
namespace FotoVision
{
	public class SettingDefaults
	{
		public static string[][] Values = new string[][]
		{
			new string[]
			{
				SettingKey.WindowPlacement.ToString(),
				"0,0,0,0"
			},
			new string[]
			{
				SettingKey.AlbumPaneWidth.ToString(),
				"184"
			},
			new string[]
			{
				SettingKey.DetailsPaneWidth.ToString(),
				"312"
			},
			new string[]
			{
				SettingKey.ImportFilterIndex.ToString(),
				"1"
			},
			new string[]
			{
				SettingKey.ShowStatusDetails.ToString(),
				"true"
			},
			new string[]
			{
				SettingKey.CloseAfterUpload.ToString(),
				"true"
			},
			new string[]
			{
				SettingKey.MaintainExifInfo.ToString(),
				"true"
			},
			new string[]
			{
				SettingKey.PublishPhotoSize.ToString(),
				"500"
			},
			new string[]
			{
				SettingKey.PublishPhotoQuality.ToString(),
				"85"
			},
			new string[]
			{
				SettingKey.EmailSubject.ToString(),
				"New photos are available"
			},
			new string[]
			{
				SettingKey.ServiceLocation.ToString(),
				"http://localhost/FotoVisionVB"
			},
			new string[]
			{
				SettingKey.ServiceTimeout.ToString(),
				"90"
			},
			new string[]
			{
				SettingKey.PromptFileDelete.ToString(),
				"true"
			},
			new string[]
			{
				SettingKey.PromptInitialMessage.ToString(),
				"true"
			}
		};
	}
}
