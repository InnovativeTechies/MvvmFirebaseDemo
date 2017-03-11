#This is a demo project for Firebase authentication on Xamarin Forms.

You have to do some steps before the usage. 

1) Create an IOS and Android application at the Firebase console

2) Also create an application with IOS and Android capabilities at the Facebook developer page.

3) Change to enabled facebook and e-mail authentication at the Authentication/sign-in methodes on the Firebase console.   

4) Download and add the GoogleService-Info.plist from the Firebase console to the MvvmDemo.IOS project.
   Change the build action to BoundleResource on this file. 

5) Change the field vaule of FacebookAppID to your facebook app id in the Info.plist (IOS project). Also Change the the string value of the "URL Schemes" to fbYourFAcebookAppId. It has to start with fb prefix.

6) Change  facebook and firebase values in the app.config file at the MvvmDemo.Core project. 

7) Maybe you have to install the newest version of proguard.jar in the Android sdk folder. Just rewrite the jar with the newest one. It is necessary for the Android multi-dex build. 