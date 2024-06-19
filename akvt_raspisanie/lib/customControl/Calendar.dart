import 'dart:collection';

import 'package:collection/collection.dart';
import 'package:akvt_raspisanie/DB/DB.dart';
import 'package:akvt_raspisanie/HelpersClasses/Lessons.dart';
import 'package:akvt_raspisanie/HelpersClasses/StudyDates.dart';
import 'package:akvt_raspisanie/customControl/Card.dart';
import 'package:flutter/material.dart';
import 'package:isar/isar.dart';
import 'package:path/path.dart';
import 'package:provider/provider.dart';
import 'package:table_calendar/table_calendar.dart';
import 'package:intl/intl.dart';
import 'package:swipe_widget/swipe_widget.dart';

import '../models/Para.dart';

class TableBasicsExample extends StatefulWidget {
  @override
  _TableBasicsExampleState createState() => _TableBasicsExampleState();
}

class _TableBasicsExampleState extends State<TableBasicsExample> {
  CalendarFormat _calendarFormat = CalendarFormat.week;
  DateTime _focusedDay = DateTime.now();
  DateTime _selectedDay = DateTime.now();
  final DateTime _firstDay = StudyDates.GetAcademicYearStart();
  // Map<DateTime, List<Para>> _eventsList = {};

  Map<int, List<Para>> _eventsListTest = {};

  // int getHashCode(DateTime key) {
  //   return key.day * 1000000 + key.month * 10000 + key.year;
  // }
  int weeksBetween() {
    DateTime from = _selectedDay;
    DateTime to = DateTime(from.year, 1, 1);
    return ((to.difference(from).inDays / 7).ceil()) % 2;
  }
  List<Para> getLessonsForSelectedDay(List<Para> lessons) {
    List<Para> firstWeekLessons = lessons.where((lesson) => lesson.weekNumber == 0).toList();
    List<Para> secondWeekLessons = lessons.where((lesson) => lesson.weekNumber == 1).toList();

    int currentWeek = weeksBetween();
    if (currentWeek == 1) {
      // Заменяем занятия первой недели на второй, кроме тех, которые относятся ко второй неделе
      firstWeekLessons.removeWhere((lesson) => secondWeekLessons.any((secondLesson) => secondLesson.lessonNumber == lesson.lessonNumber && secondLesson.weekday == lesson.weekday));
      secondWeekLessons.addAll(firstWeekLessons);
      secondWeekLessons.sort((a, b) => a.lessonNumber.compareTo(b.lessonNumber));
      return secondWeekLessons.where((lesson) => lesson.weekday == _selectedDay.weekday).toList();
    }

    return firstWeekLessons.where((lesson) => lesson.weekday == _selectedDay.weekday).toList();
  }
  void updateEventsListTest(List<Para> lessons) {
    List<Para> firstWeekLessons = lessons.where((lesson) => lesson.weekNumber == 0).toList();
    List<Para> secondWeekLessons = lessons.where((lesson) => lesson.weekNumber == 1).toList();

    Map<int, List<Para>> eventsList = {};

    for (var lesson in lessons) {
      int key = (lesson.weekNumber * 10) + lesson.weekday;
      if (eventsList[key] == null) {
        eventsList[key] = [];
      }
      eventsList[key]?.add(lesson);
    }

    // Handle the swapping logic for the second week
    int currentWeek = weeksBetween();
    if (currentWeek == 1) {
      for (var lesson in firstWeekLessons) {
        bool isOverlapping = secondWeekLessons.any((secondLesson) => secondLesson.lessonNumber == lesson.lessonNumber && secondLesson.weekday == lesson.weekday);
        if (!isOverlapping) {
          int key = (1 * 10) + lesson.weekday;
          if (eventsList[key] == null) {
            eventsList[key] = [];
          }
          eventsList[key]?.add(lesson);
        }
      }
    }

    setState(() {
      _eventsListTest = eventsList;
    });
  }

  @override
  Widget build(BuildContext context) {
    List<Para> temp = Provider.of<Lessons>(context).GetLessons2();
    // List<Para> lessons = temp.where((element) => element.weekday==_selectedDay.weekday && element.weekNumber == weeksBetween()).toList();
    List<Para> lessons = getLessonsForSelectedDay(temp);

    // _eventsList = groupBy(temp, (Para para) => DateTime(2024,3,4).add(Duration(days: para.weekday-1)));

    //
    // print(_eventsList);
    //
    // final _events = LinkedHashMap<DateTime, List>(
    //   equals: isSameDay,
    //   hashCode: getHashCode,
    // )..addAll(_eventsList);
    //
    // print( "_events ${_events}");
    //
    //
    //
    // List getEventForDay(DateTime day) {
    //   return _events[day] ?? [];
    // }

    _eventsListTest = groupBy(temp, (Para para) => (para.weekNumber * 10) + para.weekday);


    updateEventsListTest(temp);

    List getEventSample(DateTime day) {
      int currentWeek = weeksBetween();
      int key = (currentWeek * 10) + day.weekday;
      return _eventsListTest[key] ?? [];
    }

    // _eventsListTest = groupBy(temp, (Para para) => para.weekday );
    // List getEventSample(DateTime day){
    //   return _eventsListTest[day.weekday]??[];
    // }

    return Column(children: [
      TableCalendar(
      availableGestures: AvailableGestures.all,
      eventLoader: getEventSample,
      firstDay: _firstDay,
      lastDay: DateTime.utc(_firstDay.year+1, _firstDay.month, _firstDay.day),
      focusedDay: _focusedDay,
      weekNumbersVisible: true,
      calendarFormat: _calendarFormat,
      startingDayOfWeek: StartingDayOfWeek.monday,
      locale: 'ru_RU'/*Localizations.localeOf(context).languageCode*/,
      headerStyle: const HeaderStyle(
        formatButtonVisible: true,
        formatButtonShowsNext: false,

        titleTextStyle: TextStyle(fontSize: 18.0,fontFamily: 'Ubuntu'),
      ),
      daysOfWeekHeight: 24.0,
      calendarStyle: const CalendarStyle(
          outsideTextStyle: TextStyle(fontFamily: "Ubuntu",fontSize: 18,color: Color.fromRGBO(227, 228, 232, 1))
      ),
      calendarBuilders: CalendarBuilders(
        dowBuilder:(context, date) {
          return Container(
            alignment: Alignment.topCenter,
            height: 1000.0,
            child: Text(
              DateFormat.E('ru').format(date),
              style: const TextStyle(fontSize: 18.0,fontFamily: 'Ubuntu'),
            ),
          );
        },
        todayBuilder: (context, date, _) {
          return Container(
            alignment: Alignment.center,
            child: Text(
              '${date.day}',
              style: const TextStyle(fontSize: 18.0,fontFamily: 'Ubuntu'),
            ),
          );
        },
        defaultBuilder: (context, date, _) {
          return Container(
            alignment: Alignment.center,
            child: Text(
              '${date.day}',
              style: const TextStyle(fontSize: 18.0,fontFamily: 'Ubuntu'),
            ),
          );
        },
        selectedBuilder: (context, date, _) {
          return Padding(
            padding: const EdgeInsets.all(10.0),
            child: Container(
              alignment: Alignment.center,
              decoration: BoxDecoration(
                  color: const Color.fromRGBO(217, 217, 217, 100),
                  border: Border.all(
                      style: BorderStyle.none
                  ),
                  borderRadius: const BorderRadius.all(Radius.circular(40.0))
              ),
              child: Text(
                '${date.day}',
                style: const TextStyle(fontSize: 18.0,fontFamily: 'Ubuntu'),
              ),
            )
            ,);
        },
      ),
      selectedDayPredicate: (day) {

        return isSameDay(_selectedDay, day);
      },
      onDaySelected: (selectedDay, focusedDay) {
        if (!isSameDay(_selectedDay, selectedDay)) {
          setState(() {
            _selectedDay = selectedDay;
            _focusedDay = focusedDay;
          });
        }
      },
      onFormatChanged: (format) {
        if (_calendarFormat != format) {
          setState(() {
            _calendarFormat = format;
          });
        }
      },
      onPageChanged: (focusedDay) {
        setState(() {
          _focusedDay = focusedDay;

        });
      },
        onHeaderTapped: (gg){},
    ),
       Expanded(
        child: GestureDetector(
          onHorizontalDragEnd: (details){
          // switch (details.primaryVelocity){}
          if(details.velocity.pixelsPerSecond.dx < 0) {
          print("left");
          _selectedDay=_selectedDay.add(const Duration(days: 1));
          setState(() {});
            print(_selectedDay.toString());
          }
          else {
            _selectedDay=_selectedDay.subtract(const Duration(days: 1));
          print("right");
          print(_selectedDay.toString());
          setState(() {});
          }
          },
          child: lessons.isEmpty ?  /*Center(child: Text('Нет занятий'))*/ SizedBox.expand(child: Container(color: Colors.white,child:const Center(child: Text('Нет занятий')) ),
    ) :  ListView.builder(
            padding: EdgeInsets.zero,
            shrinkWrap: true,
            key:  Key(lessons.hashCode.toString()),
            itemCount: lessons.length,
            itemBuilder: (BuildContext context, int index) {
              return LessonCard(paraDB: /*lessons[index]*/ Para.ConvertorToParaDB(lessons[index]));
            },
          ),
        )
      )
    ],
    );
  }
}
// onHorizontalDragEnd: (details){
// // switch (details.primaryVelocity){}
// if(details.primaryVelocity! < 0) {
// print("left");
// _selectedDay.add(Duration(days: -1));
// setState(() {});
// }
// else {
// _selectedDay.add(Duration(days: 1));
// print("right");
// setState(() {});
// }
// },