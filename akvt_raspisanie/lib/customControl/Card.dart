import 'package:flutter_svg/flutter_svg.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../DB/DB.dart';

class LessonCard extends StatefulWidget {
  ParaDB paraDB;

  LessonCard({super.key, required this.paraDB});

  @override
  State<LessonCard> createState() => _LessonCardState(this.paraDB);
}

class _LessonCardState extends State<LessonCard> {
  ParaDB paraDB;

  _LessonCardState(this.paraDB) {
    FillTeachers();
    SwitchNumLesson(paraDB.lessonNumber);
    print(paraDB.audience.toString());
  }

  DateTime start = DateTime(1);
  DateTime end = DateTime(1);

  void SwitchNumLesson(num) {
    switch (num) {
      case 1:
        start = DateTime.utc(1, 1, 1, 8, 30, 0, 0, 0);
        end = DateTime.utc(1, 1, 1, 10, 5, 0, 0, 0);
        break;
      case 2:
        start = DateTime.utc(1, 1, 1, 10, 15, 0, 0, 0);
        end = DateTime.utc(1, 1, 1, 11, 50, 0, 0, 0);
        break;
      case 3:
        start = DateTime.utc(1, 1, 1, 12, 20, 0, 0, 0);
        end = DateTime.utc(1, 1, 1, 13, 55, 0, 0, 0);
        break;
      case 4:
        start = DateTime.utc(1, 1, 1, 14, 10, 0, 0, 0);
        end = DateTime.utc(1, 1, 1, 15, 45, 0, 0, 0);
        break;
      case 5:
        start = DateTime.utc(1, 1, 1, 15, 55, 0, 0, 0);
        end = DateTime.utc(1, 1, 1, 17, 30, 0, 0, 0);
        break;
      case 6:
        start = DateTime.utc(1, 1, 1, 17, 40, 0, 0, 0);
        end = DateTime.utc(1, 1, 1, 19, 15, 0, 0, 0);
        break;
      default:
        start = DateTime.utc(1, 1, 1, 0, 0, 0, 0, 0);
        end = DateTime.utc(1, 1, 1, 0, 0, 0, 0, 0);
    }
  }

  List<String> teachers = [];

  // String teachers = '';

  void FillTeachers() {
    paraDB.teachers?.forEach((element) {
      teachers
          .add("${element.surname} ${element.name![0]}.${element.patronymic![0]}. ");
    });
  }

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
                      child: UnconstrainedBox(
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
                          child: Row(
                            children: [
                              Center(
                                  child: Padding(
                                padding: const EdgeInsets.fromLTRB(15, 0, 0, 0),
                                child: Text(paraDB.lessonNumber.toString(),
                                    style: const TextStyle(
                                        fontSize: 16.0,
                                        fontFamily: 'Inter',
                                        color: Colors.white,
                                        fontWeight: FontWeight.normal)),
                              )),
                              Center(
                                  child: Padding(
                                padding: const EdgeInsets.fromLTRB(8, 0, 0, 0),
                                child: SvgPicture.asset(
                                    'lib/res/icons/clock.svg',
                                    color: Colors.white,
                                    width: 16.0,
                                    height: 16.0),
                              )),
                              Center(
                                  child: Padding(
                                padding: const EdgeInsets.fromLTRB(8, 0, 26, 0),
                                child: Text(
                                    '${DateFormat.Hm().format(start)}-${DateFormat.Hm().format(end)}',
                                    style: const TextStyle(
                                        fontSize: 16.0,
                                        fontFamily: 'Ubuntu',
                                        color: Colors.white,
                                        fontWeight: FontWeight.normal)),
                              )),
                            ],
                          ),
                        ),
                      )),
                ),
                Align(
                    alignment: Alignment.topLeft,
                    child: Padding(
                        padding: const EdgeInsets.fromLTRB(17, 16, 0, 0),
                        child: Expanded(
                          child: Text(paraDB.subjectName.toString(),
                              style: const TextStyle(
                                  fontSize: 16.0,
                                  fontFamily: 'Ubuntu',
                                  color: Colors.black,
                                  fontWeight: FontWeight.normal)),
                        ))),
                Align(
                    alignment: Alignment.topLeft,
                    child: Padding(
                      padding: const EdgeInsets.fromLTRB(12, 16, 0, 16),
                      child: Wrap(
                        spacing: 8.0,
                        runSpacing: 8.0,
                        children: [
                          if (paraDB.teachers!.isNotEmpty)
                            for (String teacher in teachers)
                              UnconstrainedBox(
                                child: Container(
                                    decoration: BoxDecoration(
                                        color: const Color.fromRGBO(
                                            227, 228, 232, 1),
                                        border:
                                            Border.all(style: BorderStyle.none),
                                        borderRadius: const BorderRadius.all(
                                            Radius.circular(15.0))),
                                    height: 47,
                                    child: Row(
                                      children: [
                                        Center(
                                            child: Padding(
                                          padding: const EdgeInsets.fromLTRB(
                                              8, 0, 0, 0),
                                          child: SvgPicture.asset(
                                              'lib/res/icons/user.svg',
                                              color: const Color.fromRGBO(
                                                  51, 51, 51, 1),
                                              width: 24.0,
                                              height: 24.0),
                                        )),
                                        Padding(
                                            padding: const EdgeInsets.fromLTRB(
                                                8, 0, 8, 0),
                                            child: Text(teacher,
                                                overflow: TextOverflow.ellipsis,
                                                style: const TextStyle(
                                                    fontSize: 16.0,
                                                    fontFamily: 'Ubuntu',
                                                    color: Color.fromRGBO(
                                                        51, 51, 51, 1),
                                                    fontWeight:
                                                        FontWeight.normal)))
                                      ],
                                    )),
                              )
                          else
                            UnconstrainedBox(
                              child: Container(
                                  decoration: BoxDecoration(
                                      color: const Color.fromRGBO(
                                          227, 228, 232, 1),
                                      border:
                                          Border.all(style: BorderStyle.none),
                                      borderRadius: const BorderRadius.all(
                                          Radius.circular(15.0))),
                                  height: 47,
                                  child: Row(
                                    children: [
                                      Center(
                                          child: Padding(
                                        padding: const EdgeInsets.fromLTRB(
                                            8, 0, 0, 0),
                                        child: SvgPicture.asset(
                                            'lib/res/icons/user.svg',
                                            color: const Color.fromRGBO(
                                                51, 51, 51, 1),
                                            width: 24.0,
                                            height: 24.0),
                                      )),
                                      const Padding(
                                        padding: EdgeInsets.fromLTRB(
                                            8, 0, 8, 0),
                                        child: Text('Нет преподователя',
                                            overflow: TextOverflow.ellipsis,
                                            style: TextStyle(
                                                fontSize: 16.0,
                                                fontFamily: 'Ubuntu',
                                                color: Color.fromRGBO(
                                                    51, 51, 51, 1),
                                                fontWeight: FontWeight.normal)),
                                      )
                                    ],
                                  )),
                            ),
                          // UnconstrainedBox(
                          //   child: Container(
                          //       decoration: BoxDecoration(
                          //           color:
                          //               const Color.fromRGBO(227, 228, 232, 1),
                          //           border: Border.all(style: BorderStyle.none),
                          //           borderRadius: const BorderRadius.all(
                          //               Radius.circular(15.0))),
                          //       height: 47,
                          //       child: Row(
                          //         children: [
                          //           Center(
                          //               child: Padding(
                          //             padding:
                          //                 const EdgeInsets.fromLTRB(8, 0, 0, 0),
                          //             child: SvgPicture.asset(
                          //                 'lib/res/icons/user.svg',
                          //                 color: const Color.fromRGBO(
                          //                     51, 51, 51, 1),
                          //                 width: 24.0,
                          //                 height: 24.0),
                          //           )),
                          //           Padding(
                          //               padding: const EdgeInsets.fromLTRB(
                          //                   8, 0, 8, 0),
                          //               child: Column(
                          //                 mainAxisAlignment:
                          //                     MainAxisAlignment.center,
                          //                 children: [
                          //                   if (paraDB.teachers!.isNotEmpty)
                          //                     for (String teacher in teachers)
                          //                       Text(teacher,
                          //                           overflow:
                          //                               TextOverflow.ellipsis,
                          //                           style: const TextStyle(
                          //                               fontSize: 16.0,
                          //                               fontFamily: 'Ubuntu',
                          //                               color: Color.fromRGBO(
                          //                                   51, 51, 51, 1),
                          //                               fontWeight:
                          //                                   FontWeight.normal))
                          //                   else
                          //                     const Text('Нет преподователя',
                          //                         overflow:
                          //                             TextOverflow.ellipsis,
                          //                         style: TextStyle(
                          //                             fontSize: 16.0,
                          //                             fontFamily: 'Ubuntu',
                          //                             color: Color.fromRGBO(
                          //                                 51, 51, 51, 1),
                          //                             fontWeight:
                          //                                 FontWeight.normal))
                          //                 ],
                          //               ))
                          //         ],
                          //       )),
                          // ),
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
                                      padding:
                                          const EdgeInsets.fromLTRB(8, 0, 0, 0),
                                      child: SvgPicture.asset(
                                          'lib/res/icons/marker.svg',
                                          color: const Color.fromRGBO(
                                              51, 51, 51, 1),
                                          width: 24.0,
                                          height: 24.0),
                                    )),
                                    Center(
                                        child: Padding(
                                      padding:
                                          const EdgeInsets.fromLTRB(8, 0, 8, 0),
                                      child: Text(paraDB.audience.toString(),
                                          style: const TextStyle(
                                              fontSize: 16.0,
                                              fontFamily: 'Ubuntu',
                                              color:
                                                  Color.fromRGBO(51, 51, 51, 1),
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
                                  padding:
                                      const EdgeInsets.fromLTRB(8, 0, 8, 0),
                                  child: SvgPicture.asset(
                                      'lib/res/icons/bookmark_add.svg',
                                      color:
                                          const Color.fromRGBO(51, 51, 51, 1),
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
