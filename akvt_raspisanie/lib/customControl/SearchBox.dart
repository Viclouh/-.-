import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class SearchBox extends StatelessWidget {
  const SearchBox({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
        decoration: BoxDecoration(
            color: const Color.fromRGBO(243, 243, 243, 100),
            border: Border.all(style: BorderStyle.none),
            borderRadius: const BorderRadius.all(Radius.circular(16.0))),
        child: Row(
          children: <Widget>[
            IconButton(
                icon: SvgPicture.asset('lib/res/icons/search_1.svg',
                    color: Colors.black),
                onPressed: () {}),
            Expanded(
              child: TextFormField(
                  style: const TextStyle(
                    fontSize: 16.0,
                    fontFamily: 'Ubuntu',
                    color: Colors.black
                  ),
                  decoration: const InputDecoration(
                    hintText: "Поиск",
                    focusedBorder: InputBorder.none,
                    enabledBorder: InputBorder.none,
                  )),
            ),
          ],
        ));
  }
}
