
import 'package:akvt_raspisanie/customControl/Calendar.dart';
import 'package:akvt_raspisanie/customControl/Card.dart';
import 'package:akvt_raspisanie/customControl/CustomTitle.dart';
import 'package:akvt_raspisanie/customControl/SearchBox.dart';

import 'package:flutter/material.dart';

class Home extends StatefulWidget {
  const Home({super.key});

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: <Widget>[
          const Align(
              alignment: Alignment.topLeft,
              child: Padding(
                padding: EdgeInsets.fromLTRB(18, 30, 0, 0),
                child: CustomTitle(text: 'Группа - ПБ - 11', isVisible: false),
              )),
          const Padding(
            padding: EdgeInsets.fromLTRB(8, 10, 8, 0),
            child: SearchBox(),
          ),
          Expanded(child: TableBasicsExample(),)
          // LessonCard(
          //     num_lesson: 1,
          //     start: DateTime.now(),
          //     end: DateTime.now(),
          //     lesson: 'Основы алгоритмизации и программирования',
          //     prepod: 'Растопшина Т.С',
          //     num_class: 107)
        ],
      ),
    );
  }
}
