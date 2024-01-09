
import 'package:drift/drift.dart';
import 'dart:io';
import 'package:drift/native.dart';
import 'package:path_provider/path_provider.dart';
import 'package:path/path.dart' as p;
import 'package:sqlite3/sqlite3.dart';
import 'package:sqlite3_flutter_libs/sqlite3_flutter_libs.dart';

import '../models/Para.dart';
import '../models/Teacher.dart';

part 'Notes.g.dart';


class TODOs extends Table{
   IntColumn get id =>  integer().autoIncrement()();
   TextColumn get name => text()();
   BoolColumn get isCompleted => boolean()();
   DateTimeColumn get datetime => dateTime()();
   IntColumn get idPara => integer().nullable()();
   TextColumn get description => text()();
}

@UseRowClass(Teacher)
class Teachers extends Table{
   IntColumn get id =>  integer().autoIncrement()();
   TextColumn get name => text()();
   TextColumn get firstName => text()();
   TextColumn get secondName => text()();
}

@UseRowClass(Para)
class Lessons extends Table{
   IntColumn get id =>  integer().autoIncrement()();
   TextColumn get subject => text()();
   TextColumn get cabinet => text()();
   TextColumn get group => text()();
   IntColumn get weekDay => integer()();
   IntColumn get numPara => integer()();
   IntColumn get weekNum => integer()();
   BoolColumn get isDistant => boolean()();
}

@UseRowClass(ParaTeachers)
class ParaTeachers extends Table{
   IntColumn get idPara => integer().references(Para, #id)();
   IntColumn get idTeacher => integer().references(Teachers, #id)();
}

typedef ParaWithTeachers = ({
   Para para,
   List<Teacher> teachers
});

@DriftDatabase(tables: [TODOs,Teachers,Lessons,ParaTeachers])
class AppDatabase extends _$AppDatabase {
   AppDatabase() : super(_openConnection());

   @override
   int get schemaVersion => 1;

   Future<List<TODO>> getByComplete(bool b) => (select(tODOs)..where((tbl) => tbl.isCompleted.equals(b))).get();

   Future<void> EditAllParas()async{

   }
}

LazyDatabase _openConnection() {
   return LazyDatabase(() async {

      final dbFolder = await getApplicationDocumentsDirectory();
      final file = File(p.join(dbFolder.path, 'db.sqlite'));

      if (Platform.isAndroid) {
         await applyWorkaroundToOpenSqlite3OnOldAndroidVersions();
      }
      final cachebase = (await getTemporaryDirectory()).path;
      sqlite3.tempDirectory = cachebase;
      return NativeDatabase.createInBackground(file);
   });
}