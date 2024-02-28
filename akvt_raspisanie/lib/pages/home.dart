
import 'package:akvt_raspisanie/HelpersClasses/Lessons.dart';
import 'package:akvt_raspisanie/customControl/Calendar.dart';
import 'package:akvt_raspisanie/customControl/Card.dart';
import 'package:akvt_raspisanie/customControl/CustomTitle.dart';
import 'package:akvt_raspisanie/customControl/SearchBox.dart';

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class Home extends StatefulWidget {
  const Home({super.key});

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  @override
  Widget build(BuildContext context) {
    String title = Provider.of<Lessons>(context).GetItem().item;
    return Scaffold(
      body: Column(
        children: <Widget>[
           Align(
              alignment: Alignment.topLeft,
              child: Padding(
                padding: EdgeInsets.fromLTRB(18, 30, 0, 0),
                child: CustomTitle(text: title, isVisible: false),
              )),
           Padding(
            padding: EdgeInsets.fromLTRB(8, 10, 8, 0),
            child: SearchBox(),
          ),
          Expanded(
            child: TableBasicsExample(),
          )
        ],
      ),
    );
  }
}
