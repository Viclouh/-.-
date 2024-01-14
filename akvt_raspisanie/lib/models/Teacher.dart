import 'package:isar/isar.dart';
import 'package:json_annotation/json_annotation.dart';

part 'Teacher.g.dart';

@JsonSerializable()
class Teacher{
  late int id;
  late String? name;
  late String? surname;
  late String? patronymic;

  Teacher(
      this.id,
      this.name,
      this.surname,
      this.patronymic
  );

  factory Teacher.fromJson(Map<String, dynamic> json) =>
      _$TeacherFromJson(json);
  Map<String, dynamic> toJson() => _$TeacherToJson(this);

  // factory Teacher.fromJson(dynamic json) {
  //   return Teacher(json['id'] as int, json['name'] as String,json['firstName'] as String,json['secondName'] as String);
  // }
}


