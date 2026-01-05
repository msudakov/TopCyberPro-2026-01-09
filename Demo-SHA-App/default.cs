using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace MyNamespace {
	public class MyClass : Page {
		protected System.Web.UI.WebControls.Label MyLabel;
		protected System.Web.UI.WebControls.TextBox DataTextBox;
		protected System.Web.UI.WebControls.TextBox HashTextBox;
		protected System.Web.UI.WebControls.TextBox DataTextBoxRaw;
		protected System.Web.UI.WebControls.Button SubmitButton;

		protected void Page_Load(object sender, EventArgs e) {
			string secret = "5853e81be9d44bdae27360640c52330d441aae6fb2c3d8695d25e851d1a6267f";
			if (string.IsNullOrEmpty(DataTextBox.Text.ToString()))
			{
				string input = "757365723d61747461636b65723b616374696f6e3d76696577";
				string inputString = secret + input;
				byte[] rawDataBytes = getBytes(inputString);
				DataTextBoxRaw.Text = Encoding.UTF8.GetString(getBytes(input));
				string hash = "";
				using (SHA256 sha256Hash = SHA256.Create())
				{
					byte[] hashBytes = sha256Hash.ComputeHash(rawDataBytes);
					hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
				}
				HashTextBox.Text = hash;
				DataTextBox.Text = input;
			}
		}
		
		protected void Verify(Object sender, EventArgs e) {
			string secret = "5853e81be9d44bdae27360640c52330d441aae6fb2c3d8695d25e851d1a6267f";
			string input = DataTextBox.Text.ToString();
			
			byte[] rawDataBytes = getBytes(input);
			DataTextBoxRaw.Text = Encoding.UTF8.GetString(rawDataBytes);
			
			string inputString = secret + input;
			byte[] inputBytes = getBytes(inputString);
			string hashCheck = "";
			
			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] hashBytes = sha256Hash.ComputeHash(inputBytes);
				hashCheck = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
			}
			
			if (!hashCheck.Equals(HashTextBox.Text.ToString().ToLowerInvariant()))
			{
				MyLabel.Text = "<span style='font-weight: bold; color: red;'>[-] Cookie integrity NOT verified...</span>";
			}
			else if (Encoding.UTF8.GetString(rawDataBytes).Contains("admin=true"))
			{
				MyLabel.Text = "<span style='font-weight: bold; color: green;'>[+] Cookie integrity verified!</span><br />User is <span style='font-weight: bold; color: green;'>admin</span>";
			}
			else
			{
				MyLabel.Text = "<span style='font-weight: bold; color: green;'>[+] Cookie integrity verified!</span><br />User is <span style='font-weight: bold; color: red;'>NOT</span> admin";
			}
		}

		public static byte[] getBytes(string hex) {
			int numChars = hex.Length;
			byte[] bytes = new byte[numChars / 2];
			for (int i = 0; i < numChars; i += 2) {
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			}
			return bytes;
		}
	}
}
