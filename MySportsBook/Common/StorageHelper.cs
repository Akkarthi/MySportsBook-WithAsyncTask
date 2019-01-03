using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences;

namespace MySportsBook
{
    public class StorageHelper
    {
        #region Declaration
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;
        #endregion

        #region Storage Helper        
        public StorageHelper(Context context)
        {
            mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }
        public string GetStringFromStorage(string keyName)
        {
            return mSharedPrefs.GetString(keyName, string.Empty);
        }
        public bool GetBoolFromStorage(string keyName)
        {
            return mSharedPrefs.GetBoolean(keyName, false);
        }

        internal void SaveStringInStorage(string keyName, string value)
        {
            mPrefsEditor.PutString(keyName, value);
            mPrefsEditor.Commit();
        }
        internal void SaveBoolInStorage(string keyName, bool value)
        {
            mPrefsEditor.PutBoolean(keyName, value);
            mPrefsEditor.Commit();
        }
        #endregion
    }
}