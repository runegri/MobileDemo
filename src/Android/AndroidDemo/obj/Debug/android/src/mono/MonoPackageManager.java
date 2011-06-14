package mono;

import java.io.*;
import java.lang.String;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	public static void LoadApplication (Context context, String runtimeDataDir, String[] apks)
	{
		synchronized (lock) {
			if (!initialized) {
				System.loadLibrary("monodroid");
				java.util.Locale locale = java.util.Locale.getDefault ();

				Runtime.init (
						locale.getLanguage () + "-" + locale.getCountry (),
						apks,
						runtimeDataDir,
						new String[]{
							context.getFilesDir ().getAbsolutePath (),
							context.getCacheDir ().getAbsolutePath (),
							context.getApplicationInfo ().dataDir + "/lib",
						},
						context.getClassLoader (),
						MonoPackageManager_Resources.Assemblies);
				initialized = true;
			}
		}
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		"AndroidDemo.dll",
		"AndroidDemoLib.dll",
		"Applicable.Android.dll",
	};
	public static final String[] Dependencies = new String[]{
		"Applicable.Android.dll",
		"AndroidDemoLib.dll",
	};
	public static final String ApiPackageName = "Mono.Android.Platform.ApiLevel_8";
}
