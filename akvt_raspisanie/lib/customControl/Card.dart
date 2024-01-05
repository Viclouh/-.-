import 'package:flutter_svg/flutter_svg.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class LessonCard extends StatefulWidget {
  int num_lesson;
  DateTime start;
  DateTime end;
  String lesson;
  String prepod;
  int num_class;

  LessonCard(
      {super.key,
      required this.num_lesson,
      required this.start,
      required this.end,
      required this.lesson,
      required this.num_class,
      required this.prepod});

  @override
  State<LessonCard> createState() => _LessonCardState(this.num_lesson,
      this.start, this.end, this.lesson, this.num_class, this.prepod);
}

class _LessonCardState extends State<LessonCard> {
  int num_lesson;

  DateTime start;
  DateTime end;
  String lesson;
  String prepod;
  int num_class;

  _LessonCardState(this.num_lesson, this.start, this.end, this.lesson,
      this.num_class, this.prepod);

  @override
  Widget build(BuildContext context) {
    return Padding(
        padding: const EdgeInsets.fromLTRB(8, 16, 8, 0),
        child: Container(
            decoration: BoxDecoration(
                color: const Color.fromRGBO(243, 243, 243, 1),
                border: Border.all(style: BorderStyle.none),
                borderRadius: const BorderRadius.all(Radius.circular(16.0))),
            width: double.infinity,
            child: Column(
              children: [
                Align(
                  alignment: Alignment.topLeft,
                  child: Padding(
                      padding: const EdgeInsets.fromLTRB(0, 16, 0, 0),
                      child: Container(
                        decoration: BoxDecoration(
                            color: const Color.fromRGBO(173, 38, 185, 1),
                            border: Border.all(style: BorderStyle.none),
                            borderRadius: const BorderRadius.only(
                                topLeft: Radius.circular(4.0),
                                topRight: Radius.circular(16.0),
                                bottomLeft: Radius.circular(4.0),
                                bottomRight: Radius.circular(16.0))),
                        height: 32,
                        width: 200,
                        child: Row(
                          children: [
                            Center(
                                child: Padding(
                              padding: EdgeInsets.fromLTRB(15, 0, 0, 0),
                              child: Text('$num_lesson',
                                  style: TextStyle(
                                      fontSize: 20.0,
                                      fontFamily: 'Inter',
                                      color: Colors.white,
                                      fontWeight: FontWeight.normal)),
                            )),
                            Center(
                                child: Padding(
                              padding: EdgeInsets.fromLTRB(8, 0, 0, 0),
                              child: SvgPicture.asset('lib/res/icons/clock.svg',
                                  color: Colors.white,
                                  width: 16.0,
                                  height: 16.0),
                            )),
                            Center(
                                child: Padding(
                              padding: EdgeInsets.fromLTRB(8, 0, 26, 0),
                              child: Text(
                                  '${DateFormat.Hm().format(start)}-${DateFormat.Hm().format(end)}',
                                  style: TextStyle(
                                      fontSize: 20.0,
                                      fontFamily: 'Ubuntu',
                                      color: Colors.white,
                                      fontWeight: FontWeight.normal)),
                            )),
                          ],
                        ),
                      )),
                ),
                Align(
                    alignment: Alignment.topLeft,
                    child: Padding(
                        padding: const EdgeInsets.fromLTRB(17, 16, 0, 0),
                        child: Expanded(
                          child: Text('$lesson',
                              style: TextStyle(
                                  fontSize: 20.0,
                                  fontFamily: 'Ubuntu',
                                  color: Colors.black,
                                  fontWeight: FontWeight.normal)),
                        ))),
                Align(
                    alignment: Alignment.topLeft,
                    child: Padding(
                      padding: const EdgeInsets.fromLTRB(17, 16, 0, 16),
                      child: Wrap(
                        spacing: 8.0,
                        runSpacing: 8.0,
                        children: [
                          UnconstrainedBox(
                            child: Container(
                                decoration: BoxDecoration(
                                    color:
                                        const Color.fromRGBO(227, 228, 232, 1),
                                    border: Border.all(style: BorderStyle.none),
                                    borderRadius: const BorderRadius.all(
                                        Radius.circular(15.0))),
                                height: 47,
                                child: Row(
                                  children: [
                                    Center(
                                        child: Padding(
                                      padding: EdgeInsets.fromLTRB(8, 0, 0, 0),
                                      child: SvgPicture.asset(
                                          'lib/res/icons/user.svg',
                                          color: Color.fromRGBO(51, 51, 51, 1),
                                          width: 24.0,
                                          height: 24.0),
                                    )),
                                    Center(
                                        child: Padding(
                                      padding: EdgeInsets.fromLTRB(8, 0, 8, 0),
                                      child: Text('$prepod',
                                          style: TextStyle(
                                              fontSize: 20.0,
                                              fontFamily: 'Ubuntu',
                                              color: Color.fromRGBO(51, 51, 51, 1),
                                              fontWeight: FontWeight.normal)),
                                    )),
                                  ],
                                )),
                          ),
                          UnconstrainedBox(
                            child: Container(
                                decoration: BoxDecoration(
                                    color:
                                        const Color.fromRGBO(227, 228, 232, 1),
                                    border: Border.all(style: BorderStyle.none),
                                    borderRadius: const BorderRadius.all(
                                        Radius.circular(15.0))),
                                height: 47,
                                child: Row(
                                  children: [
                                    Center(
                                        child: Padding(
                                      padding: EdgeInsets.fromLTRB(8, 0, 0, 0),
                                      child: SvgPicture.asset(
                                          'lib/res/icons/marker.svg',
                                          color:  Color.fromRGBO(51, 51, 51, 1),
                                          width: 24.0,
                                          height: 24.0),
                                    )),
                                    Center(
                                        child: Padding(
                                      padding: EdgeInsets.fromLTRB(8, 0, 8, 0),
                                      child: Text('$num_class',
                                          style: TextStyle(
                                              fontSize: 20.0,
                                              fontFamily: 'Ubuntu',
                                              color:  Color.fromRGBO(51, 51, 51, 1),
                                              fontWeight: FontWeight.normal)),
                                    )),
                                  ],
                                )),
                          ),
                          UnconstrainedBox(
                            child: Container(
                                decoration: BoxDecoration(
                                    color:
                                        const Color.fromRGBO(227, 228, 232, 1),
                                    border: Border.all(style: BorderStyle.none),
                                    borderRadius: const BorderRadius.all(
                                        Radius.circular(15.0))),
                                height: 47,
                                child: Center(
                                    child: Padding(
                                  padding: EdgeInsets.fromLTRB(8, 0, 8, 0),
                                  child: SvgPicture.asset(
                                      'lib/res/icons/bookmark_add.svg',
                                      color:  Color.fromRGBO(51, 51, 51, 1),
                                      width: 24.0,
                                      height: 24.0),
                                ))),
                          ),
                        ],
                      ),
                    ))
              ],
            )));
  }
}
