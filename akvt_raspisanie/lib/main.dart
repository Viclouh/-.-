
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


void main() {
  String s1 = "kitten";
  String s2 = "sitting";

  int distance = LevenshteinDistance.levenshteinDistance(s1, s2);
  print("Редакционное расстояние между $s1 и $s2: $distance");


  runApp(
  MultiProvider(
    providers:[
      ChangeNotifierProvider(create:(context) =>  Lessons())
    ],
    child: MaterialApp(
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
    )
      // home:SplashScreen(),
    ),
  );
}


