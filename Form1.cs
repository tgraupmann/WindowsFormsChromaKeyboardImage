using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Color = Corale.Colore.Core.Color;

namespace WindowsFormsChromaKeyboardImage
{
    public partial class Form1 : Form
    {
        const string KEY_CHROMA_IMAGE = "CHROMA_IMAGE";
        const string KEY_IMAGE = "IMAGE";

        private string _mFileName = string.Empty;

        private class KeyData
        {
            public Key _mKey;
            public Color _mColor;
            public static implicit operator KeyData(Key key)
            {
                KeyData keyData = new KeyData();
                keyData._mKey = key;
                keyData._mColor = Color.Black;
                return keyData;
            }
        }

        #region Key layout

        private static KeyData[,] _sKeys =
        {
            {
                Key.Tab,
                Key.Q,
                Key.W,
                Key.E,
                Key.R,
                Key.T,
                Key.Y,
                Key.U,
                Key.I,
                Key.O,
                Key.P,
                Key.Oem4, //[
                Key.Oem5, //]
                Key.Oem6, //\
                Key.Delete,
                Key.End,
                Key.PageDown,
                Key.Num7,
                Key.Num8,
                Key.Num9,
            },
            {
                Key.CapsLock,
                Key.A,
                Key.S,
                Key.D,
                Key.F,
                Key.G,
                Key.H,
                Key.J,
                Key.K,
                Key.L,
                Key.Oem7, //;
                Key.Oem8, //'
                Key.Enter,
                Key.Invalid,
                Key.Invalid,
                Key.Invalid,
                Key.Invalid,
                Key.Num4,
                Key.Num5,
                Key.Num6,
            },
            {
                Key.LeftShift,
                Key.Z,
                Key.X,
                Key.C,
                Key.V,
                Key.B,
                Key.N,
                Key.M,
                Key.Oem9, //,
                Key.Oem10, //.
                Key.Oem11, //?
                Key.RightShift,
                Key.Invalid,
                Key.Invalid,
                Key.Invalid,
                Key.Up,
                Key.Invalid,
                Key.Num1,
                Key.Num2,
                Key.Num3,
            },
        };

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void _mButtonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _mButtonLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "image files (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (!string.IsNullOrEmpty(_mFileName))
            {
                openFileDialog1.FileName = _mFileName;
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(_mFileName);
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _mFileName = openFileDialog1.FileName;
                _mPicture.Image = Image.FromFile(_mFileName);

                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(KEY_CHROMA_IMAGE);
                key.SetValue(KEY_IMAGE, _mFileName);
                key.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _mPicture.SizeMode = PictureBoxSizeMode.StretchImage;

            Microsoft.Win32.RegistryKey key;
            foreach (string name in Microsoft.Win32.Registry.CurrentUser.GetSubKeyNames())
            {
                if (name == KEY_CHROMA_IMAGE)
                {
                    key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(KEY_CHROMA_IMAGE);
                    if (null != key)
                    {
                        _mFileName = (string)key.GetValue(KEY_IMAGE);
                        if (!string.IsNullOrEmpty(_mFileName))
                        {
                            _mPicture.Image = Image.FromFile(_mFileName);
                        }
                    }
                }
            }
        }

        private static void SetColor(Key key, Color color)
        {
            if (key != Key.Invalid)
            {
                Keyboard.Instance.SetKey(key, color);
            }
        }

        private void _mPicture_Click(object sender, EventArgs e)
        {
            Image image = _mPicture.Image;
            if (null == image)
            {
                return;
            }

            Bitmap bitmap = image as Bitmap;
            if (null == bitmap)
            {
                return;
            }

            MouseEventArgs evt = e as MouseEventArgs;
            int minX = (int)(evt.X / (float)_mPicture.Width * bitmap.Width);
            int minY = (int)(evt.Y / (float)_mPicture.Height * bitmap.Height);


            /*
            if (minX < bitmap.Width &&
                minY < bitmap.Height)
            {
                bitmap.SetPixel(minX, minY, System.Drawing.Color.Red);
                _mPicture.Image = bitmap;
            }
            */

            int x = minX;
            for (int i = 0; i < _sKeys.GetLength(0); ++i, ++x)
            {
                int y = minY;
                for (int j = 0; j < _sKeys.GetLength(1); ++j, ++y)
                {
                    System.Drawing.Color c1 = System.Drawing.Color.Black;
                    if (x < bitmap.Width &&
                        y < bitmap.Height)
                    {
                        c1 = bitmap.GetPixel(x, y);
                    }
                    KeyData keyData = _sKeys[i, j];
                    keyData._mColor = new Color(c1.R, c1.G, c1.B);
                    SetColor(keyData._mKey, keyData._mColor);
                }
                ++minX;
            }
        }
    }
}
