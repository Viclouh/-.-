import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';


class SearchBox extends StatelessWidget {
  const SearchBox({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
        padding: const EdgeInsets.symmetric(horizontal:8.0,vertical: 34.0),
        child:Container(
          decoration: BoxDecoration(
            border: Border.all(),
            borderRadius: const BorderRadius.all(
                Radius.circular(5.0)
            )
          ),
          child:Row(
            children: <Widget>[
              IconButton( icon: SvgPicture.asset('lib/res/icons/search_1.svg',color: Colors.black), onPressed: () {  }),
              Expanded(
                  child:TextFormField(
                      decoration: const InputDecoration(
                        hintText: "Поиск",
                        focusedBorder: InputBorder.none,
                        enabledBorder: InputBorder.none,


                      )
                  ),
              ),
            ],
          )
        )
    );
  }
}
