import 'package:akvt_raspisanie/models/Speciality.dart';
import 'package:json_annotation/json_annotation.dart';
import '../DB/DB.dart';

part 'Group.g.dart';

@JsonSerializable(explicitToJson: true)
class Group{
  late int id;
  late Speciality? speciality;
  late String? name;

  Group(this.id, this.speciality, this.name);

  factory Group.fromJson(Map<String, dynamic> json) =>
      _$GroupFromJson(json);
  Map<String, dynamic> toJson() =>  _$GroupToJson(this);

  static Item ConvertorToItem (Group group){
    return Item(group.id,'${group.speciality!.shortname} - ${group.name}', 'group');
  }
}