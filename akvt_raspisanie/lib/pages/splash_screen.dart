import 'package:flutter/material.dart';

class SplashScreen extends StatelessWidget {
  const SplashScreen ({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body:
          Center(
            child: Image(
              image: AssetImage('lib/res/images/logo.png'),
              height: 270,
              width: 330,
            ),
          )
    );
  }
}
