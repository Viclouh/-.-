import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CustomTitle extends StatelessWidget {
  final String text;
  final bool isVisible;
  const CustomTitle({super.key, required this.text,required this.isVisible});

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        Container(
          // width: double.infinity,
          // height: 24.0,
          child: Text(
            text,
            textAlign: TextAlign.center,
            style: const TextStyle(
              fontSize: 18.0,
              fontWeight: FontWeight.w700,
              fontFamily: 'Ubuntu',
            ),
          ),
        ),
        Visibility(
            child: IconButton(onPressed: (){}, icon:SvgPicture.asset('lib/res/icons/edit.svg')),
          visible: isVisible,
        )

      ],
    );
  }
}
