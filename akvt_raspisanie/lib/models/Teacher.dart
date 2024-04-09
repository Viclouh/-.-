import 'package:json_annotation/json_annotation.dart';

import '../DB/DB.dart';
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

  static Item ConvertorToItem (Teacher teacher){
    return Item(teacher.id,'${teacher.surname} ${teacher.name}.${teacher.patronymic}.', 'teacher');
  }
}


