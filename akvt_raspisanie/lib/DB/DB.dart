import 'dart:convert';

import 'package:akvt_raspisanie/models/Group.dart';
import 'package:akvt_raspisanie/models/Teacher.dart';
import 'package:isar/isar.dart';
import 'package:path_provider/path_provider.dart';
import 'package:http/http.dart' as http;

import '../models/Audience.dart';
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


class Item{
  Id id ;
  String item;
  String type;
  Item(this.id, this.item, this.type);

}
@collection
class ItemDB{
  Id id  = Isar.autoIncrement;
  int itemID;
  String item;
  String type;
  ItemDB( this.itemID, this.item, this.type);
  ItemDB.Full( this.id, this.itemID, this.item, this.type);

  static Item ConvertorToItem (ItemDB itemDB){
    return Item(itemDB.itemID,itemDB.item, itemDB.type);
  }
}

class AppDB{
  static final Future<Isar> _isar = Init();
  static Future<Isar> get isar => _isar;

  static Future<Isar> Init()async{
    final dir = await getApplicationDocumentsDirectory();
    final isar = await Isar.open(
      [ParaDBSchema,NoteSchema,ItemDBSchema],
      directory: dir.path,
    );
    return isar;
  }

  Future<List<Para>> FetchLessons() async {
    final response = await http
        .get(Uri.parse('http://hnt8.ru:1149/api/LessonPlan?formatting=MobileApp'));
    if (response.statusCode == 200) {
      Iterable iterable = jsonDecode(response.body);
      return iterable.map((e) => Para.fromJson(e)).toList();
    } else {
      throw Exception('Failed to load album');
    }
  }

  static Future<List<Audience>> FetchAudience() async {
    final response = await http
        .get(Uri.parse('http://hnt8.ru:1149/api/Audience'));
    if (response.statusCode == 200) {
      Iterable iterable = jsonDecode(response.body);
      return iterable.map((e) => Audience.fromJson(e)).toList();
    } else {
      throw Exception('Failed to load album');
    }
  }

  static Future<List<Teacher>> FetchTeacher() async {
    final response = await http
        .get(Uri.parse('http://hnt8.ru:1149/api/Teacher'));
    if (response.statusCode == 200) {
      Iterable iterable = jsonDecode(response.body);
      return iterable.map((e) => Teacher.fromJson(e)).toList();
    } else {
      throw Exception('Failed to load album');
    }
  }
  static Future<List<Group>> FetchGroup() async {
    final response = await http
        .get(Uri.parse('http://hnt8.ru:1149/api/Group'));
    if (response.statusCode == 200) {
      Iterable iterable = jsonDecode(response.body);
      return iterable.map((e) => Group.fromJson(e)).toList();
    } else {
      throw Exception('Failed to load album');
    }
  }

  Future<void> EditAllParas()async{

    final isar =await AppDB.isar;
    // isar.paraDBs.clear();
    int count  = await isar.paraDBs.count();
    if(count==0){
      List<Para> paraObjs =  await FetchLessons();
      paraObjs.forEach((element) {
        isar.writeTxn(() async {
          isar.paraDBs.put( Para.ConvertorToParaDB(element));
        });
      });
    }
    // debugPrint((await isar.paraDBs.count()).toString());
  }


  static Future<List<Item>> EditAllItems()async{

    List<Audience> audiences =  await FetchAudience();
    List<Teacher> teachers =  await FetchTeacher();
    List<Group> groups =  await FetchGroup();

    List<Item> items = [];

    audiences.forEach((element) {
        items.add( Audience.ConvertorToItem(element));
    });
    teachers.forEach((element) {
      items.add( Teacher.ConvertorToItem(element));
    });
    groups.forEach((element) {
      items.add( Group.ConvertorToItem(element));
    });
    return items;
  }
  static Future<List<Item>> EditGroupsAndTeachers()async{
    List<Group> groups =  await FetchGroup();

    List<Item> items = [];
    List<Teacher> teachers =  await FetchTeacher();

    teachers.forEach((element) {
      items.add( Teacher.ConvertorToItem(element));
    });

    groups.forEach((element) {
      items.add( Group.ConvertorToItem(element));
    });
    return items;
  }

}