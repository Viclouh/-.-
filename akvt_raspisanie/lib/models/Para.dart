import 'dart:ffi';

import 'package:akvt_raspisanie/DB/DB.dart';
import 'package:isar/isar.dart';
import 'package:akvt_raspisanie/models/Teacher.dart';
import 'package:json_annotation/json_annotation.dart';

part 'Para.g.dart';

@JsonSerializable(explicitToJson: true)
class Para {
  int id;
  String subjectName;
  String? audience;
  String group;
  int weekday;
  int lessonNumber;
  int weekNumber;
  bool isDistantce;
  List<Teacher>? teachers;

  Para(
    this.id,
    this.lessonNumber,
    this.subjectName,
    this.audience,
    this.group,
    this.isDistantce,
    this.weekNumber,
    this.weekday,
    this.teachers
);


  factory Para.fromJson(Map<String, Object?> json) =>
      _$ParaFromJson(json);

  Map<String, dynamic> toJson() => _$ParaToJson(this);

  static ParaDB ConvertorToParaDB (Para para){

    var paraDb = ParaDB()
      ..id = para.id
      ..subjectName = para.subjectName
      ..group = para.group
      ..weekday= para.weekday
      ..audience = para.audience
      ..isDistantce = para.isDistantce
      ..lessonNumber = para.lessonNumber;

    if(para.teachers!=null){
      List<TeacherDB> _teachers = [];
      para.teachers?.forEach((element) {
        _teachers.add(TeacherDB()
          ..id =element.id
          ..name =  element.name
          ..patronymic = element.patronymic
          ..surname = element.surname);
      });
      paraDb.teachers = _teachers;
    }

    return paraDb;
  }

  // factory Para.fromJson(dynamic json){
  //   var teacherObjsJson = json['teachers'] as List;
  //   List<Teacher> _teachers = teacherObjsJson.map((tagJson) => Teacher.fromJson(tagJson)).toList();
  //   return Para(
  //     json['id'] as int,
  //     json['numPara'] as int,
  //     json['subject'] as String,
  //     json['audience'] as String,
  //     json['group'] as String,
  //     json['isDistant'] as bool,
  //     json['weekNum'] as int,
  //     json['weekDay'] as int,
  //     _teachers
  //   );
  // }

}