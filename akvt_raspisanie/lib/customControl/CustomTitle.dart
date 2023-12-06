import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CustomTitle extends StatelessWidget {
  final String text;
  const CustomTitle({super.key,required this.text});

  @override
  Widget build(BuildContext context) {
    return Align(
      alignment: Alignment.center,
        child: Container(
          width: double.infinity,
          height: 24.0,
          child: Text(
            text,
            textAlign: TextAlign.center,
            style: const TextStyle(fontSize: 20.0,fontFamily: 'Ubuntu',),

          ),
        ),
    );
  }
}
