import 'package:json_annotation/json_annotation.dart';
part 'Speciality.g.dart';

@JsonSerializable()
class Speciality{
  late int id;
  late String? code;
  late String? name;
  late String? shortname;


  Speciality(this.id, this.code, this.name, this.shortname);

  factory Speciality.fromJson(Map<String, dynamic> json) =>
      _$SpecialityFromJson(json);
  Map<String, dynamic> toJson() => _$SpecialityToJson(this);
}