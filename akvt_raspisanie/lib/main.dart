
import 'package:akvt_raspisanie/HelpersClasses/Lessons.dart';
import 'package:akvt_raspisanie/pages/edit_note.dart';
import 'package:akvt_raspisanie/HelpersClasses/LevenshteinDistance.dart';
import 'package:flutter/material.dart';
import 'package:akvt_raspisanie/pages/home.dart';
import 'package:akvt_raspisanie/pages/splash_screen.dart';
import 'package:akvt_raspisanie/pages/navigation.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:path/path.dart';
import 'package:provider/provider.dart';
import 'package:onesignal_flutter/onesignal_flutter.dart';


void main() {
  runApp(
  MultiProvider(
    providers:[
      ChangeNotifierProvider(create:(context) =>  Lessons())
    ],
    child: MyApp()
      // home:SplashScreen(),
    ),
  );

  // OneSignal.Debug.setLogLevel(OSLogLevel.verbose);

  // OneSignal.initialize("915cc389-bc1e-403d-8f15-86d7dbc0463e");
  // OneSignal.Notifications.requestPermission(true);
  // String TagKey = Lessons().GetGroup().type;
  // String TagValue = Lessons().GetGroup().item;
  // OneSignal.User.addTagWithKey(TagKey,TagValue);


}
class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {

    OneSignal.initialize("915cc389-bc1e-403d-8f15-86d7dbc0463e");
    OneSignal.Notifications.requestPermission(true);

    String TagKey = Provider.of<Lessons>(context).GetGroup().type;
    int TagValue = Provider.of<Lessons>(context).GetGroup().id;
    OneSignal.User.addTagWithKey(TagKey,TagValue);

    return MaterialApp(
      theme: ThemeData(
          primaryColor: Colors.white
      ),
      localizationsDelegates: const [
        GlobalMaterialLocalizations.delegate,
      ],
      supportedLocales: const [
        Locale('en', ''), //
        Locale('ru', ''), //
      ],
      debugShowCheckedModeBanner: false,
      initialRoute: '/navigation',
      routes: {
        // '/':(context)=> SplashScreen(),
        '/navigation':(context) => Navigation(),
        '/editNote':(context) => EditNote(),

      },
    );
  }
}


