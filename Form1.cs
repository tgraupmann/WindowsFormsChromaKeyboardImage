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
        private const string KEY_CHROMA_IMAGE = "CHROMA_IMAGE";
        private const string KEY_IMAGE = "IMAGE";
        private const string ITEM_BLACK_WIDOW = "Razer BlackWidow Chroma";
        private const string ITEM_BLADE = "Blade Chroma";

        private bool _mLoadingTexture = false;
        private string _mFileName = string.Empty;

        private int _mMinX = 0;
        private int _mMinY = 0;
        private int _mMaxX = 0;
        private int _mMaxY = 0;

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

        private static KeyData[,] _sKeys = null;

        private static readonly KeyData[,] KEYS_BLACK_WINDOW =
        {
            {
                Key.Invalid,
                Key.Escape,
                Key.Invalid,
                Key.Invalid,
                Key.F1,
                Key.F2,
                Key.F3,
                Key.F4,
                Key.F5,
                Key.F6,
                Key.F7,
                Key.F8,
                Key.F9,
                Key.F10,
                Key.F11,
                Key.F12,
                Key.PrintScreen,
                Key.Scroll,
                Key.Pause,
                Key.Invalid,
                Key.Invalid,
                Key.Invalid,
            },

            {
                Key.Macro1,
                Key.Oem1, //~
                Key.One,
                Key.Two,
                Key.Three,
                Key.Four,
                Key.Five,
                Key.Six,
                Key.Seven,
                Key.Eight,
                Key.Nine,
                Key.Zero,
                Key.Oem2, //-
                Key.Oem3, //+
                Key.Backspace,
                Key.Insert,
                Key.Home,
                Key.PageUp,
                Key.NumLock,
                Key.NumDivide,
                Key.NumMultiply,
                Key.NumSubtract,
            },

            {
                Key.Macro2,
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
                Key.NumAdd,
            },
            {
                Key.Macro3,
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
                Key.NumAdd,
            },
            {
                Key.Macro4,
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
                Key.NumEnter,
            },

            {
                Key.Macro5,
                Key.LeftControl,
                Key.LeftWindows,
                Key.LeftAlt,
                Key.Space,
                Key.Space,
                Key.Space,
                Key.Space,
                Key.Space,
                Key.Space,
                Key.RightAlt,
                Key.Function,
                Key.RightMenu,
                Key.RightControl,
                Key.Left,
                Key.Down,
                Key.Right,
                Key.Num0,
                Key.Num0,
                Key.NumDecimal,
                Key.NumEnter,
                Key.Invalid,
            },
        };

        private static readonly KeyData[,] KEYS_BLADE =
        {
            {
                Key.Escape,
                Key.Invalid,
                Key.Invalid,
                Key.F1,
                Key.F2,
                Key.F3,
                Key.F4,
                Key.F5,
                Key.F6,
                Key.F7,
                Key.F8,
                Key.F9,
                Key.F10,
                Key.F11,
                Key.F12,
                Key.PrintScreen,
                Key.Scroll,
                Key.Pause,
            },

            {
                Key.Oem1, //~
                Key.One,
                Key.Two,
                Key.Three,
                Key.Four,
                Key.Five,
                Key.Six,
                Key.Seven,
                Key.Eight,
                Key.Nine,
                Key.Zero,
                Key.Oem2, //-
                Key.Oem3, //+
                Key.Backspace,
                Key.Insert,
                Key.Home,
                Key.PageUp,
                Key.Invalid,
            },

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
                Key.Invalid,
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
                Key.Invalid,
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
                Key.Invalid,
            },

            {
                Key.LeftControl,
                Key.LeftWindows,
                Key.LeftAlt,
                Key.Space,
                Key.Space,
                Key.Space,
                Key.Space,
                Key.Space,
                Key.Space,
                Key.RightAlt,
                Key.Function,
                Key.RightMenu,
                Key.RightControl,
                Key.Left,
                Key.Down,
                Key.Right,
                Key.Invalid,
                Key.Invalid,
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

        private void LoadImage()
        {
            if (null != _mPicture.Image)
            {
                _mPicture.Image.Dispose();
            }

            if (!File.Exists(_mFileName))
            {
                return;
            }

            _mPicture.Image = Image.FromFile(_mFileName);

            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(KEY_CHROMA_IMAGE);
            key.SetValue(KEY_IMAGE, _mFileName);
            key.Close();

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

            _mMinX = 0;
            _mMinY = 0;
            _mMaxX = bitmap.Width - 1;
            _mMaxY = bitmap.Height - 1;
        }

        private void _mButtonLoadImage_Click(object sender, EventArgs e)
        {
            _mLoadingTexture = true;

            if (null != _mPicture.Image)
            {
                _mPicture.Image.Dispose();
                _mPicture.Image = null;
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "image files (*.jpg;*.gif)|*.jpg;*.gif";
            if (string.IsNullOrEmpty(_mFileName))
            {
                openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog1.FileName = string.Empty;
            }
            else
            {
                openFileDialog1.FileName = _mFileName;
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(_mFileName);
            }

            try
            {
                _mFileName = null;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _mFileName = openFileDialog1.FileName;
                    LoadImage();
                    DisplayImageOnKeyboard();
                }
            }
            catch (ArgumentException)
            {
                _mFileName = null;
            }

            _mLoadingTexture = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _mComboBoxLayout.Items.Clear();
            _mComboBoxLayout.Items.Add(ITEM_BLACK_WIDOW);
            _mComboBoxLayout.Items.Add(ITEM_BLADE);
            _mComboBoxLayout.SelectedIndex = 0;

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
                        LoadImage();
                    }
                }
            }

            DisplayImageOnKeyboard();

            _mPicture.MouseDown += _mPicture_MouseDown;
            _mPicture.MouseUp += _mPicture_MouseUp;

            _mTimerAnimation.Start();
        }

        private static void SetColor(Key key, Color color)
        {
            if (key != Key.Invalid)
            {
                Keyboard.Instance.SetKey(key, color);
            }
        }

        private void _mPicture_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
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

            int minX = (int)(e.X / (float)_mPicture.Width * bitmap.Width);
            int minY = (int)(e.Y / (float)_mPicture.Height * bitmap.Height);
            if (minX < bitmap.Width &&
                minY < bitmap.Height)
            {
                _mMinX = minX;
                _mMinY = minY;
            }
            else
            {
                return;
            }

            /*
            bitmap.SetPixel(_mMinX, _mMinY, System.Drawing.Color.Green);
            _mPicture.Image = bitmap;
            */

            return;
        }

        private void _mPicture_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadImage();

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
            int x = (int)(evt.X / (float)_mPicture.Width * bitmap.Width);
            int y = (int)(evt.Y / (float)_mPicture.Height * bitmap.Height);

            int minX = Math.Min(_mMinX, x);
            if (minX < 0 || minX >= bitmap.Width)
            {
                return;
            }

            int minY = Math.Min(_mMinY, y);
            if (minY < 0 || minY >= bitmap.Height)
            {
                return;
            }

            int maxX = Math.Max(_mMinX, x);
            if (maxX < 0 || maxX >= bitmap.Width)
            {
                return;
            }

            int maxY = Math.Max(_mMinY, y);
            if (maxY < 0 || maxY >= bitmap.Height)
            {
                return;
            }

            if (minX == maxX ||
                minY == maxY)
            {
                minX = 0;
                minY = 0;
                maxX = bitmap.Width - 1;
                maxY = bitmap.Height - 1;
            }

            _mMinX = minX;
            _mMinY = minY;
            _mMaxX = maxX;
            _mMaxY = maxY;

            // invert outside area
            for (x = 0; x < bitmap.Width; ++x)
            {
                for (y = 0; y < bitmap.Height; ++y)
                {
                    if (x < minX || x > maxX ||
                        y < minY || y > maxY)
                    {
                        System.Drawing.Color c = bitmap.GetPixel(x, y);
                        c = System.Drawing.Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B);
                        bitmap.SetPixel(x, y, c);
                    }
                }
            }

            _mPicture.Image = bitmap;

            /*
            bitmap.SetPixel(x, y, System.Drawing.Color.Red);
            _mPicture.Image = bitmap;
            */

            DisplayImageOnKeyboard();
        }

        private void DisplayImageOnKeyboard()
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

            if (string.IsNullOrEmpty(_mFileName))
            {
                return;
            }

            if (_mMinX == _mMaxX ||
                _mMinY == _mMaxY)
            {
                _mMinX = 0;
                _mMinY = 0;
                _mMaxX = bitmap.Width - 1;
                _mMaxY = bitmap.Height - 1;
            }

            for (int i = 0; i < _sKeys.GetLength(0); ++i)
            {
                float ratioI = i / (float)_sKeys.GetLength(0);
                int y = _mMinY + (int)(ratioI * (_mMaxY - _mMinY));
                for (int j = 0; j < _sKeys.GetLength(1); ++j)
                {
                    float ratioJ = j / (float)_sKeys.GetLength(1);
                    int x = _mMinX + (int)(ratioJ * (_mMaxX - _mMinX));
                    System.Drawing.Color color = System.Drawing.Color.Black;
                    if (x < bitmap.Width &&
                        y < bitmap.Height)
                    {
                        color = bitmap.GetPixel(x, y);
                    }
                    KeyData keyData = _sKeys[i, j];
                    keyData._mColor = new Color(color.R, color.G, color.B);
                    SetColor(keyData._mKey, keyData._mColor);
                }
            }
        }

        private void _mComboBoxLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mComboBoxLayout.SelectedIndex < 0)
            {
                return;
            }
            string item = (string)_mComboBoxLayout.Items[_mComboBoxLayout.SelectedIndex];
            switch (item)
            {
                case ITEM_BLACK_WIDOW:
                    _sKeys = KEYS_BLACK_WINDOW;
                    break;
                case ITEM_BLADE:
                    _sKeys = KEYS_BLADE;
                    break;
                default:
                    return;
            }

            DisplayImageOnKeyboard();
        }

        private void _mTimerAnimation_Tick(object sender, EventArgs e)
        {
            if (_mLoadingTexture)
            {
                return;
            }
            DisplayImageOnKeyboard();
        }
    }
}
