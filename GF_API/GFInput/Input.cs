using SDL2;
using System.Runtime.InteropServices;

namespace GF_API.GFInput
{
    public static class Input
    {
        // TODO: Make a keyboard control system.
        private static byte[] prevKeysUp;
        private static byte[] prevKeysDown;
        
        /// <summary>
        /// 
        /// </summary>
        public static bool GetKey(KeyCode code)
        {
            IntPtr origArray = SDL.SDL_GetKeyboardState(out int arraySize);
            byte[] keys = new byte[arraySize];
            byte keycode = (byte)SDL.SDL_GetScancodeFromKey((SDL.SDL_Keycode)code);
            Marshal.Copy(origArray, keys, 0, arraySize);
            return keys[keycode] == 1;
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool GetKeyDown(KeyCode code)
        {
            IntPtr origArray = SDL.SDL_GetKeyboardState(out int arraySize);
            byte[] keys = new byte[arraySize];

            if (prevKeysDown == null) prevKeysDown = new byte[arraySize];

            byte keycode = (byte)SDL.SDL_GetScancodeFromKey((SDL.SDL_Keycode)code);
            Marshal.Copy(origArray, keys, 0, arraySize);
            bool keyPressed = keys[keycode] == 1 && prevKeysDown[keycode] == 0;
            prevKeysDown = (byte[])keys.Clone();
            return keyPressed;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool GetKeyUp(KeyCode code)
        {
            IntPtr origArray = SDL.SDL_GetKeyboardState(out int arraySize);
            byte[] keys = new byte[arraySize];
            
            if(prevKeysUp == null) prevKeysUp = new byte[arraySize];

            byte keycode = (byte)SDL.SDL_GetScancodeFromKey((SDL.SDL_Keycode)code);
            Marshal.Copy(origArray, keys, 0, arraySize);
            bool keyRealized = keys[keycode] == 0 && prevKeysUp[keycode] == 1;
            prevKeysUp = (byte[])keys.Clone();
            return keyRealized;
        }

        public enum KeyCode
        {
            Unknow = SDL.SDL_Keycode.SDLK_UNKNOWN,
            Return = SDL.SDL_Keycode.SDLK_RETURN,
            Escape = SDL.SDL_Keycode.SDLK_ESCAPE,
            Backspace = SDL.SDL_Keycode.SDLK_BACKSPACE,
            Tab = SDL.SDL_Keycode.SDLK_TAB,
            Space = SDL.SDL_Keycode.SDLK_SPACE,
            Exclaim = SDL.SDL_Keycode.SDLK_EXCLAIM,
            Quotedbl = SDL.SDL_Keycode.SDLK_QUOTEDBL,
            Hash = SDL.SDL_Keycode.SDLK_HASH,
            Dollar = SDL.SDL_Keycode.SDLK_DOLLAR,
            Percent = SDL.SDL_Keycode.SDLK_PERCENT,
            Ampersand = SDL.SDL_Keycode.SDLK_AMPERSAND,
            Quote = SDL.SDL_Keycode.SDLK_QUOTE,
            Leftparen = SDL.SDL_Keycode.SDLK_LEFTPAREN,
            Rightparen = SDL.SDL_Keycode.SDLK_RIGHTPAREN,
            Asterisk = SDL.SDL_Keycode.SDLK_ASTERISK,
            Plus = SDL.SDL_Keycode.SDLK_PLUS,
            Comma = SDL.SDL_Keycode.SDLK_COMMA,
            Minus = SDL.SDL_Keycode.SDLK_MINUS,
            Period = SDL.SDL_Keycode.SDLK_PERIOD,
            Slash = SDL.SDL_Keycode.SDLK_SLASH,
            Num0 = SDL.SDL_Keycode.SDLK_0,
            Num1 = SDL.SDL_Keycode.SDLK_1,
            Num2 = SDL.SDL_Keycode.SDLK_2,
            Num3 = SDL.SDL_Keycode.SDLK_3,
            Num4 = SDL.SDL_Keycode.SDLK_4,
            Num5 = SDL.SDL_Keycode.SDLK_5,
            Num6 = SDL.SDL_Keycode.SDLK_6,
            Num7 = SDL.SDL_Keycode.SDLK_7,
            Num8 = SDL.SDL_Keycode.SDLK_8,
            Num9 = SDL.SDL_Keycode.SDLK_9,
            Colon = SDL.SDL_Keycode.SDLK_COLON,
            Semicolon = SDL.SDL_Keycode.SDLK_SEMICOLON,
            Less = SDL.SDL_Keycode.SDLK_LESS,
            Equals = SDL.SDL_Keycode.SDLK_EQUALS,
            Greater = SDL.SDL_Keycode.SDLK_GREATER,
            Question = SDL.SDL_Keycode.SDLK_QUESTION,
            At = SDL.SDL_Keycode.SDLK_AT,
            Leftbracket = SDL.SDL_Keycode.SDLK_LEFTBRACKET,
            Backslash = SDL.SDL_Keycode.SDLK_BACKSLASH,
            Rightbracket = SDL.SDL_Keycode.SDLK_RIGHTBRACKET,
            Caret = SDL.SDL_Keycode.SDLK_CARET,
            Underscore = SDL.SDL_Keycode.SDLK_UNDERSCORE,
            Backquote = SDL.SDL_Keycode.SDLK_BACKQUOTE,
            A = SDL.SDL_Keycode.SDLK_a,
            B = SDL.SDL_Keycode.SDLK_b,
            C = SDL.SDL_Keycode.SDLK_c,
            D = SDL.SDL_Keycode.SDLK_d,
            E = SDL.SDL_Keycode.SDLK_e,
            F = SDL.SDL_Keycode.SDLK_f,
            G = SDL.SDL_Keycode.SDLK_g,
            H = SDL.SDL_Keycode.SDLK_h,
            I = SDL.SDL_Keycode.SDLK_i,
            J = SDL.SDL_Keycode.SDLK_j,
            K = SDL.SDL_Keycode.SDLK_k,
            L = SDL.SDL_Keycode.SDLK_l,
            M = SDL.SDL_Keycode.SDLK_m,
            N = SDL.SDL_Keycode.SDLK_n,
            O = SDL.SDL_Keycode.SDLK_o,
            P = SDL.SDL_Keycode.SDLK_p,
            Q = SDL.SDL_Keycode.SDLK_q,
            R = SDL.SDL_Keycode.SDLK_r,
            S = SDL.SDL_Keycode.SDLK_s,
            T = SDL.SDL_Keycode.SDLK_t,
            U = SDL.SDL_Keycode.SDLK_u,
            V = SDL.SDL_Keycode.SDLK_v,
            W = SDL.SDL_Keycode.SDLK_w,
            X = SDL.SDL_Keycode.SDLK_x,
            Y = SDL.SDL_Keycode.SDLK_y,
            Z = SDL.SDL_Keycode.SDLK_z,
            Capslock = SDL.SDL_Keycode.SDLK_CAPSLOCK,
            F1 = SDL.SDL_Keycode.SDLK_F1,
            F2 = SDL.SDL_Keycode.SDLK_F2,
            F3 = SDL.SDL_Keycode.SDLK_F3,
            F4 = SDL.SDL_Keycode.SDLK_F4,
            F5 = SDL.SDL_Keycode.SDLK_F5,
            F6 = SDL.SDL_Keycode.SDLK_F6,
            F7 = SDL.SDL_Keycode.SDLK_F7,
            F8 = SDL.SDL_Keycode.SDLK_F8,
            F9 = SDL.SDL_Keycode.SDLK_F9,
            F10 = SDL.SDL_Keycode.SDLK_F10,
            F11 = SDL.SDL_Keycode.SDLK_F11,
            F12 = SDL.SDL_Keycode.SDLK_F12,
            Printscreen = SDL.SDL_Keycode.SDLK_PRINTSCREEN,
            Scrolllock = SDL.SDL_Keycode.SDLK_SCROLLLOCK,
            Pause = SDL.SDL_Keycode.SDLK_PAUSE,
            Insert = SDL.SDL_Keycode.SDLK_INSERT,
            Home = SDL.SDL_Keycode.SDLK_HOME,
            Pageup = SDL.SDL_Keycode.SDLK_PAGEUP,
            Delete = SDL.SDL_Keycode.SDLK_DELETE,
            End = SDL.SDL_Keycode.SDLK_END,
            Pagedown = SDL.SDL_Keycode.SDLK_PAGEDOWN,
            Right = SDL.SDL_Keycode.SDLK_RIGHT,
            Left = SDL.SDL_Keycode.SDLK_LEFT,
            Down = SDL.SDL_Keycode.SDLK_DOWN,
            Up = SDL.SDL_Keycode.SDLK_UP,
            Numlockclear = SDL.SDL_Keycode.SDLK_NUMLOCKCLEAR,
            KpDivide = SDL.SDL_Keycode.SDLK_KP_DIVIDE,
            KpMultiply = SDL.SDL_Keycode.SDLK_KP_MULTIPLY,
            KpMinus = SDL.SDL_Keycode.SDLK_KP_MINUS,
            KpPlus = SDL.SDL_Keycode.SDLK_KP_PLUS,
            KpEnter = SDL.SDL_Keycode.SDLK_KP_ENTER,
            Kp1 = SDL.SDL_Keycode.SDLK_KP_1,
            Kp2 = SDL.SDL_Keycode.SDLK_KP_2,
            Kp3 = SDL.SDL_Keycode.SDLK_KP_3,
            Kp4 = SDL.SDL_Keycode.SDLK_KP_4,
            Kp5 = SDL.SDL_Keycode.SDLK_KP_5,
            Kp6 = SDL.SDL_Keycode.SDLK_KP_6,
            Kp7 = SDL.SDL_Keycode.SDLK_KP_7,
            Kp8 = SDL.SDL_Keycode.SDLK_KP_8,
            Kp9 = SDL.SDL_Keycode.SDLK_KP_9,
            Kp0 = SDL.SDL_Keycode.SDLK_KP_0,
            KpPeriod = SDL.SDL_Keycode.SDLK_KP_PERIOD,
            Application = SDL.SDL_Keycode.SDLK_APPLICATION,
            Power = SDL.SDL_Keycode.SDLK_POWER,
            KpEquals = SDL.SDL_Keycode.SDLK_KP_EQUALS,
            F13 = SDL.SDL_Keycode.SDLK_F13,
            F14 = SDL.SDL_Keycode.SDLK_F14,
            F15 = SDL.SDL_Keycode.SDLK_F15,
            F16 = SDL.SDL_Keycode.SDLK_F16,
            F17 = SDL.SDL_Keycode.SDLK_F17,
            F18 = SDL.SDL_Keycode.SDLK_F18,
            F19 = SDL.SDL_Keycode.SDLK_F19,
            F20 = SDL.SDL_Keycode.SDLK_F20,
            F21 = SDL.SDL_Keycode.SDLK_F21,
            F22 = SDL.SDL_Keycode.SDLK_F22,
            F23 = SDL.SDL_Keycode.SDLK_F23,
            F24 = SDL.SDL_Keycode.SDLK_F24,
            Execute = SDL.SDL_Keycode.SDLK_EXECUTE,
            Help = SDL.SDL_Keycode.SDLK_HELP,
            Menu = SDL.SDL_Keycode.SDLK_MENU,
            Select = SDL.SDL_Keycode.SDLK_SELECT,
            Stop = SDL.SDL_Keycode.SDLK_STOP,
            Again = SDL.SDL_Keycode.SDLK_AGAIN,
            Undo = SDL.SDL_Keycode.SDLK_UNDO,
            Cut = SDL.SDL_Keycode.SDLK_CUT,
            Copy = SDL.SDL_Keycode.SDLK_COPY,
            Paste = SDL.SDL_Keycode.SDLK_PASTE,
            Find = SDL.SDL_Keycode.SDLK_FIND,
            Mute = SDL.SDL_Keycode.SDLK_MUTE,
            Volumeup = SDL.SDL_Keycode.SDLK_VOLUMEUP,
            Volumedown = SDL.SDL_Keycode.SDLK_VOLUMEDOWN,
            KpComma = SDL.SDL_Keycode.SDLK_KP_COMMA,
            KpEqualsas400 = SDL.SDL_Keycode.SDLK_KP_EQUALSAS400,
            Alterase = SDL.SDL_Keycode.SDLK_ALTERASE,
            Sysreq = SDL.SDL_Keycode.SDLK_SYSREQ,
            Cancel = SDL.SDL_Keycode.SDLK_CANCEL,
            Clear = SDL.SDL_Keycode.SDLK_CLEAR,
            Prior = SDL.SDL_Keycode.SDLK_PRIOR,
            Return2 = SDL.SDL_Keycode.SDLK_RETURN2,
            Separator = SDL.SDL_Keycode.SDLK_SEPARATOR,
            Out = SDL.SDL_Keycode.SDLK_OUT,
            Oper = SDL.SDL_Keycode.SDLK_OPER,
            Clearagain = SDL.SDL_Keycode.SDLK_CLEARAGAIN,
            Crssel = SDL.SDL_Keycode.SDLK_CRSEL,
            Exsel = SDL.SDL_Keycode.SDLK_EXSEL,
            Kp00 = SDL.SDL_Keycode.SDLK_KP_00,
            Kp000 = SDL.SDL_Keycode.SDLK_KP_000,
            Thousandsseparator = SDL.SDL_Keycode.SDLK_THOUSANDSSEPARATOR,
            Decimalseparator = SDL.SDL_Keycode.SDLK_DECIMALSEPARATOR,
            Currencyunit = SDL.SDL_Keycode.SDLK_CURRENCYUNIT,
            Currencysubunit = SDL.SDL_Keycode.SDLK_CURRENCYSUBUNIT,
            KpLeftparen = SDL.SDL_Keycode.SDLK_KP_LEFTPAREN,
            KpRightparen = SDL.SDL_Keycode.SDLK_KP_RIGHTPAREN,
            KpLeftbrace = SDL.SDL_Keycode.SDLK_KP_LEFTBRACE,
            KpRightbrace = SDL.SDL_Keycode.SDLK_KP_RIGHTBRACE,
            KpTab = SDL.SDL_Keycode.SDLK_KP_TAB,
            KpBackspace = SDL.SDL_Keycode.SDLK_KP_BACKSPACE,
            KpA = SDL.SDL_Keycode.SDLK_KP_A,
            KpB = SDL.SDL_Keycode.SDLK_KP_B,
            KpC = SDL.SDL_Keycode.SDLK_KP_C,
            KpD = SDL.SDL_Keycode.SDLK_KP_D,
            KpE = SDL.SDL_Keycode.SDLK_KP_E,
            KpF = SDL.SDL_Keycode.SDLK_KP_F,
            KpXor = SDL.SDL_Keycode.SDLK_KP_XOR,
            KpPower = SDL.SDL_Keycode.SDLK_KP_POWER,
            KpPercent = SDL.SDL_Keycode.SDLK_KP_PERCENT,
            KpLess = SDL.SDL_Keycode.SDLK_KP_LESS,
            KpGreater = SDL.SDL_Keycode.SDLK_KP_GREATER,
            KpAmpersand = SDL.SDL_Keycode.SDLK_KP_AMPERSAND,
            KpDblampersand = SDL.SDL_Keycode.SDLK_KP_DBLAMPERSAND,
            KpVerticalbar = SDL.SDL_Keycode.SDLK_KP_VERTICALBAR,
            KpDblverticalbar = SDL.SDL_Keycode.SDLK_KP_DBLVERTICALBAR,
            KpColon = SDL.SDL_Keycode.SDLK_KP_COLON,
            KpHash = SDL.SDL_Keycode.SDLK_KP_HASH,
            KpSpace = SDL.SDL_Keycode.SDLK_KP_SPACE,
            KpAt = SDL.SDL_Keycode.SDLK_KP_AT,
            KpExclam = SDL.SDL_Keycode.SDLK_KP_EXCLAM,
            KpMemstore = SDL.SDL_Keycode.SDLK_KP_MEMSTORE,
            KpMemrecall = SDL.SDL_Keycode.SDLK_KP_MEMRECALL,
            KpMemclear = SDL.SDL_Keycode.SDLK_KP_MEMCLEAR,
            KpMemadd = SDL.SDL_Keycode.SDLK_KP_MEMADD,
            KpMemsubtract = SDL.SDL_Keycode.SDLK_KP_MEMSUBTRACT,
            KpMemmultiply = SDL.SDL_Keycode.SDLK_KP_MEMMULTIPLY,
            KpMemdivide = SDL.SDL_Keycode.SDLK_KP_MEMDIVIDE,
            KpPlusminus = SDL.SDL_Keycode.SDLK_KP_PLUSMINUS,
            KpClear = SDL.SDL_Keycode.SDLK_KP_CLEAR,
            KpClearentry = SDL.SDL_Keycode.SDLK_KP_CLEARENTRY,
            KpBinary = SDL.SDL_Keycode.SDLK_KP_BINARY,
            KpOctal = SDL.SDL_Keycode.SDLK_KP_OCTAL,
            KpDecimal = SDL.SDL_Keycode.SDLK_KP_DECIMAL,
            KpHexadecimal = SDL.SDL_Keycode.SDLK_KP_HEXADECIMAL,
            Lctrl = SDL.SDL_Keycode.SDLK_LCTRL,
            Lshift = SDL.SDL_Keycode.SDLK_LSHIFT,
            Lalt = SDL.SDL_Keycode.SDLK_LALT,
            Lgui = SDL.SDL_Keycode.SDLK_LGUI,
            Rctrl = SDL.SDL_Keycode.SDLK_RCTRL,
            Rshift = SDL.SDL_Keycode.SDLK_RSHIFT,
            Ralt = SDL.SDL_Keycode.SDLK_RALT,
            Rgui = SDL.SDL_Keycode.SDLK_RGUI,
            Mode = SDL.SDL_Keycode.SDLK_MODE,
            Audionext = SDL.SDL_Keycode.SDLK_AUDIONEXT,
            Audioprev = SDL.SDL_Keycode.SDLK_AUDIOPREV,
            Audiostop = SDL.SDL_Keycode.SDLK_AUDIOSTOP,
            Audioplay = SDL.SDL_Keycode.SDLK_AUDIOPLAY,
            Audiomute = SDL.SDL_Keycode.SDLK_AUDIOMUTE,
            Mediaselect = SDL.SDL_Keycode.SDLK_MEDIASELECT,
            Www = SDL.SDL_Keycode.SDLK_WWW,
            Mail = SDL.SDL_Keycode.SDLK_MAIL,
            Calculator = SDL.SDL_Keycode.SDLK_CALCULATOR,
            Computer = SDL.SDL_Keycode.SDLK_COMPUTER,
            AcSearch = SDL.SDL_Keycode.SDLK_AC_SEARCH,
            AcHome = SDL.SDL_Keycode.SDLK_AC_HOME,
            AcBack = SDL.SDL_Keycode.SDLK_AC_BACK,
            AcForward = SDL.SDL_Keycode.SDLK_AC_FORWARD,
            AcStop = SDL.SDL_Keycode.SDLK_AC_STOP,
            AcRefresh = SDL.SDL_Keycode.SDLK_AC_REFRESH,
            AcBookmarks = SDL.SDL_Keycode.SDLK_AC_BOOKMARKS,
            Brightnessdown = SDL.SDL_Keycode.SDLK_BRIGHTNESSDOWN,
            Brightnessup = SDL.SDL_Keycode.SDLK_BRIGHTNESSUP,
            Displayswitch = SDL.SDL_Keycode.SDLK_DISPLAYSWITCH,
            Kbdillumtoggle = SDL.SDL_Keycode.SDLK_KBDILLUMTOGGLE,
            Kbdillumdown = SDL.SDL_Keycode.SDLK_KBDILLUMDOWN,
            Kbdillumup = SDL.SDL_Keycode.SDLK_KBDILLUMUP,
            Eject = SDL.SDL_Keycode.SDLK_EJECT,
            Sleep = SDL.SDL_Keycode.SDLK_SLEEP,
            App1 = SDL.SDL_Keycode.SDLK_APP1,
            App2 = SDL.SDL_Keycode.SDLK_APP2,
            Audiorrewind = SDL.SDL_Keycode.SDLK_AUDIOREWIND,
            Audiofastforward = SDL.SDL_Keycode.SDLK_AUDIOFASTFORWARD,
        }
    }
}
