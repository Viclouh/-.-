import 'package:akvt_raspisanie/customControl/Calendar.dart';
import 'package:akvt_raspisanie/customControl/Card.dart';
import 'package:akvt_raspisanie/customControl/CustomTitle.dart';
import 'package:akvt_raspisanie/customControl/SearchBox.dart';
import 'package:akvt_raspisanie/models/Cabinet.dart';
import 'package:akvt_raspisanie/models/Corpus.dart';
import 'package:akvt_raspisanie/models/Direction.dart';
import 'package:akvt_raspisanie/models/Group.dart';
import 'package:akvt_raspisanie/models/Para.dart';
import 'package:akvt_raspisanie/models/Speciality.dart';
import 'package:akvt_raspisanie/models/Subject.dart';
import 'package:akvt_raspisanie/models/TypeCabinet.dart';
import 'package:flutter/material.dart';

class Home extends StatefulWidget {
  const Home({super.key});

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  // Para para1 = Para(
  //     1,
  //     1,
  //     Subject(1, 'Основы алгоритмизации', ''),
  //     Cabinet(1, 1, Corpus(1, 'Пирогова'), TypeCabinet(1, 'Лаборатория')),
  //     Group(1, Direction(1, Speciality(1, 11), 'name', "shortName")),
  //   true
  // );

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: <Widget>[
          const Align(
              alignment: Alignment.topLeft,
              child: Padding(
                padding: EdgeInsets.fromLTRB(18, 12, 0, 0),
                child: CustomTitle(text: 'Группа - ПБ-11'),
              )),
          const Padding(
            padding: EdgeInsets.fromLTRB(8, 10, 8, 0),
            child: SearchBox(),
          ),
          TableBasicsExample(),
          LessonCard(
              num_lesson: 1,
              start: DateTime.now(),
              end: DateTime.now(),
              lesson: 'Основы алгоритмизации и программирования',
              prepod: 'Растопшина Т.С',
              num_class: 107)
        ],
      ),
    );
  }
}
