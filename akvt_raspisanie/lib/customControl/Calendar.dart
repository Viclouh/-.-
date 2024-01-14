import 'package:akvt_raspisanie/DB/DB.dart';
import 'package:akvt_raspisanie/HelpersClasses/StudyDates.dart';
import 'package:akvt_raspisanie/customControl/Card.dart';
import 'package:flutter/material.dart';
import 'package:isar/isar.dart';
import 'package:table_calendar/table_calendar.dart';
import 'package:intl/intl.dart';



class TableBasicsExample extends StatefulWidget {
  @override
  _TableBasicsExampleState createState() => _TableBasicsExampleState();
}

class _TableBasicsExampleState extends State<TableBasicsExample> {

  _TableBasicsExampleState(){
    final db = AppDB();
    db.EditAllParas().then((value){
      FillOrgerByGroupAndWeekDay(_selectedDay.weekday, 'ИБ - 42').then((value) => setState(() {}));
      print(lessons.length);
      FillAllLessons();
      print(allLessons);
    });
  }
  int allLessons  = 0;
  List<ParaDB> lessons = [];

  Future<void> FillAllLessons () async{
    final isar = await AppDB.isar;
    allLessons = await isar.paraDBs.count();
  }

  Future<void> FillOrgerByGroupAndWeekDay (int weekday,String group) async{
    final isar = await AppDB.isar;
    lessons = await isar.paraDBs.where().weekdayGroupEqualTo(weekday, group).findAll();
  }

  CalendarFormat _calendarFormat = CalendarFormat.week;
  DateTime _focusedDay = DateTime.now();
  DateTime _selectedDay = DateTime.now();
  final DateTime _firstDay = StudyDates.GetAcademicYearStart();





  @override
  Widget build(BuildContext context) {
    return Column(children: [
      TableCalendar(
      firstDay: _firstDay,
      lastDay: DateTime.utc(_firstDay.year+1, _firstDay.month, _firstDay.day),
      focusedDay: _focusedDay,
      calendarFormat: _calendarFormat,
      startingDayOfWeek: StartingDayOfWeek.monday,
      locale: Localizations.localeOf(context).languageCode,
      headerStyle: const HeaderStyle(
        formatButtonVisible: false,
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
            padding: EdgeInsets.all(10.0),
            child: Container(
              alignment: Alignment.center,
              decoration: BoxDecoration(
                  color: Color.fromRGBO(217, 217, 217, 100),
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
        // Use `selectedDayPredicate` to determine which day is currently selected.
        // If this returns true, then `day` will be marked as selected.

        // Using `isSameDay` is recommended to disregard
        // the time-part of compared DateTime objects.
        return isSameDay(_selectedDay, day);
      },
      onDaySelected: (selectedDay, focusedDay) {
        if (!isSameDay(_selectedDay, selectedDay)) {
          setState(() {
            _selectedDay = selectedDay;
            _focusedDay = focusedDay;
          });
          FillOrgerByGroupAndWeekDay(_selectedDay.weekday, 'ИБ - 42').then((value) => setState(() {}));
        }
      },
      onFormatChanged: (format) {
        if (_calendarFormat != format) {
          // Call `setState()` when updating calendar format
          setState(() {
            _calendarFormat = format;
          });
        }
      },
      onPageChanged: (focusedDay) {
        _focusedDay = focusedDay;
      },
    ),
      Expanded(
        child: lessons.isEmpty ? Center(child: Text('Нет занятий')) :  ListView.builder(
          shrinkWrap: true,
          key:  Key(lessons.hashCode.toString()),
          itemCount: lessons.length,
          itemBuilder: (BuildContext context, int index) {
            return LessonCard(paraDB: lessons[index]);
          },
        ),
      ),
    ],
    );
  }


}