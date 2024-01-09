import 'package:akvt_raspisanie/pages/edit_note.dart';
import 'package:flutter/material.dart';
import 'package:akvt_raspisanie/pages/home.dart';
import 'package:akvt_raspisanie/pages/splash_screen.dart';
import 'package:akvt_raspisanie/pages/navigation.dart';
import 'package:flutter_localizations/flutter_localizations.dart';


void main() => runApp(MaterialApp(
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
    '/':(context)=> SplashScreen(),
    '/navigation':(context) => Navigation(),
    '/editNote':(context) => EditNote(),
  },
  // home:SplashScreen(),
));


