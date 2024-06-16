import 'package:akvt_raspisanie/DB/DB.dart';
import 'package:json_annotation/json_annotation.dart';
part 'Audience.g.dart';

@JsonSerializable()
class Audience{
  late int id;
  late String? number;
  late String? audienceTypeId;
  late String? audienceType;


  Audience(this.id, this.number, this.audienceTypeId, this.audienceType);

  factory Audience.fromJson(Map<String, dynamic> json) =>
      _$AudienceFromJson(json);
  Map<String, dynamic> toJson() => _$AudienceToJson(this);

  static Item ConvertorToItem (Audience audience){
    return Item(audience.id,audience.number!, 'audience');
  }
}