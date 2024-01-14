import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:isar/isar.dart';
import 'package:path_provider/path_provider.dart';
import 'package:http/http.dart' as http;

import '../models/Para.dart';

part 'DB.g.dart';

@collection
class ParaDB {
  Id? id = Isar.autoIncrement;
  String? subjectName;
  String? audience;

  @Index()
  String? group;

  @Index(composite: [CompositeIndex('group')])
  int? weekday;

  int? lessonNumber;

  int? weekNumber;
  bool? isDistantce;
  List<TeacherDB>? teachers;
}

@embedded
class TeacherDB{
  int? id ;
  String? name;
  String? surname;
  String? patronymic;
}

@collection
class Note{
  Id id = Isar.autoIncrement;
  String name;
  @Index()
  bool isCompleted;
  DateTime dateTime;
  String description;

  Note.Full(this.id ,this.name, this.isCompleted, this.dateTime, this.description);
  Note(this.name, this.isCompleted, this.dateTime, this.description);
}

class AppDB{
  static final Future<Isar> _isar = Init();
  static Future<Isar> get isar => _isar;

  static Future<Isar> Init()async{
    final dir = await getApplicationDocumentsDirectory();
    final isar = await Isar.open(
      [ParaDBSchema,NoteSchema],
      directory: dir.path,
    );
    return isar;
  }

   Future<List<Para>> fetchLessons() async {
    final response = await http
        .get(Uri.parse('http://hnt8.ru:1149/api/LessonPlan?formatting=MobileApp'));
    if (response.statusCode == 200) {
      Iterable iterable = jsonDecode(response.body);
      return iterable.map((e) => Para.fromJson(e)).toList();
    } else {
      throw Exception('Failed to load album');
    }
  }
    Future<void> EditAllParas()async{

    final isar =await AppDB.isar;
    List<Para> paraObjs =  await fetchLessons();
    paraObjs.forEach((element) {
      isar.writeTxn(() async {
        isar.paraDBs.put( Para.ConvertorToParaDB(element));
      });
    });
    debugPrint((await isar.paraDBs.count()).toString());
  }


}