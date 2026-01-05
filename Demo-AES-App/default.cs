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
		protected System.Web.UI.WebControls.TextBox CipherTextBox;
		protected System.Web.UI.WebControls.Button SubmitButton;

		protected void Page_Load(object sender, EventArgs e) {
			if (IsPostBack)
			{
				return; 
			}
			string key = "fe9f647be28c46de68e19398270b31268ba15d68fc24c11215fd83bc10209149";
			String response = "";
			string c = Request.QueryString["c"];
			if (c == null) {
				Response.Redirect("./?c=a81d5cff84c7badbf53bad80df3c542ba78b0abc8beedea1f7545232c38baed2");
			}
			else {
				CipherTextBox.Text = c;
				string m = null;
				try {
					m = decrypt(getBytes(c), getBytes(key));
				}
				catch (Exception ex) {
					response = "<span style='color: red; font-weight: bold'>INVALID (padding did not compute...)</span>";
				}
				if (m != null) {
					response = "VALID";
				}
			}
			MyLabel.Text = response;
		}
		
		protected void Verify(Object sender, EventArgs e) {
			string key = "fe9f647be28c46de68e19398270b31268ba15d68fc24c11215fd83bc10209149";
			String response = "";
			String c = CipherTextBox.Text.ToString().ToLower();
			string m = null;
			try {
				m = decrypt(getBytes(c), getBytes(key));
			}
			catch (Exception ex) {
				response = "<span style='color: red; font-weight: bold'>INVALID (padding did not compute...)</span>";
			}
			if (m != null) {
				response = "VALID";
			}
			MyLabel.Text = response;
		}
		
		public static string getHex(byte[] bytes) {
			StringBuilder hex = new StringBuilder(bytes.Length * 2);
			foreach (byte b in bytes) {
				hex.AppendFormat("{0:x2}", b);
			}
			return hex.ToString();
		}

		public static byte[] getBytes(string hex) {
			int numChars = hex.Length;
			byte[] bytes = new byte[numChars / 2];
			for (int i = 0; i < numChars; i += 2) {
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			}
			return bytes;
		}
		
        public static byte[] encrypt(string m, byte[] k1) {
            byte[] IV = new byte[16];
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(IV);
            }
            byte[] c;

            using (AesManaged AES = new AesManaged()) {
                AES.Mode = CipherMode.CBC;
                AES.KeySize = 256;
                AES.Padding = PaddingMode.PKCS7;
                AES.Key = k1;
                AES.IV = IV;
                ICryptoTransform encryptor = AES.CreateEncryptor(AES.Key, AES.IV);
                using (MemoryStream msEncrypt = new MemoryStream()) {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                            swEncrypt.Write(m);
                        }
                        c = msEncrypt.ToArray();
                    }
                }
            }

            byte[] cipher = new byte[c.Length + IV.Length + 32];
            byte[] cAndIV = new byte[c.Length + 16];
            for (int i = 0; i < IV.Length; i++) {
                cAndIV[i] = IV[i];
            }

            for (int i = 0; i < c.Length; i++) {
                cAndIV[i + 16] = c[i];
            }

			return cAndIV;
        }

		public static string decrypt(byte[] ciphertext, byte[] key) {
			string plaintext = null;
			byte[] IV = new byte[16];
			byte[] c = new byte[ciphertext.Length - 16];
			Array.Copy(ciphertext, 0, IV, 0, 16);
			Array.Copy(ciphertext, 16, c, 0, c.Length);
			using (AesManaged AES = new AesManaged()) {
				AES.Mode = CipherMode.CBC;
				AES.KeySize = 256;
				AES.Padding = PaddingMode.PKCS7;
				AES.Key = key;
				AES.IV = IV;
				ICryptoTransform decryptor = AES.CreateDecryptor(AES.Key, AES.IV);
				using (MemoryStream msDecrypt = new MemoryStream(c)) {
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
						using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}
			return plaintext;
		}
	}
}
