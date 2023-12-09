import 'package:flutter/material.dart';
import 'package:akvt_raspisanie/pages/home.dart';
import 'package:akvt_raspisanie/pages/splash_screen.dart';
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
  home:Home(),
  // home:SplashScreen(),
));


