
import 'package:akvt_raspisanie/customControl/NoteCard.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import '../DB/Notes.dart';
import '../customControl/CustomTitle.dart';
import '../customControl/SearchBox.dart';
import '../models/Note.dart';

class Notes extends StatefulWidget {
  const Notes({super.key});

  @override
  State<Notes> createState() => _NotesState();
}

class _NotesState extends State<Notes> {

  final database  = AppDatabase();

  static Note note1 = Note(1, false, DateTime.now(), null, "ббvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv гей","гг просрали");
  static Note note2 = Note(2, false, DateTime.now(), null, "бб лох","ZZZZZ");
  static Note note3 = Note(3, true, DateTime.now(), null, "бб норм чел","Чучмек");
  static Note note4 = Note(1, false, DateTime.now(), null, "ббvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv гей","гг просрали");
  static Note note5 = Note(2, false, DateTime.now(), null, "бб лох","ZZZZZ");
  static Note note6 = Note(3, true, DateTime.now(), null, "бб норм чел","Чучмек");
  static Note note7 = Note(1, false, DateTime.now(), null, "ббvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv гей","гг просрали");
  static Note note8 = Note(2, false, DateTime.now(), null, "бб лох","ZZZZZ");
  static Note note9 = Note(3, true, DateTime.now(), null, "бб норм чел","Чучмек");

  List<Note> notes = [note1,note2,note3,note4,note5,note6,note7,note8,note9,];
  List<TODO> temp  = [];

  late Color _firstButtonColor = Color.fromRGBO(173, 38, 185, 1);
  late Color _secondButtonColor = Color.fromRGBO(227, 228, 232, 1);
  late Color _firstTextColor = Colors.white;
  late Color _secondTextColor = Colors.black87;

  Future<void> Fill () async{
    database.delete(database.tODOs).go();
    notes.forEach((element)
    {
      database.into(database.tODOs).insert(TODOsCompanion.insert(
        name: element.name,
        description: element.description,
        isCompleted: element.isCompleted,
        datetime: element.dateTime,
      ));
    }
    );
    temp = await database.select(database.tODOs).get();
  }
  Future<void> FillOrgerByIsCompleted (bool b) async{
    temp  = b ? await database.getByComplete(b) : await database.getByComplete(b);
  }


  _NotesState() {
    FillOrgerByIsCompleted(false).then((value) => setState(() {}));
  }


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: <Widget>[
           const Align(
              alignment: Alignment.topLeft,
              child: Padding(
                padding: EdgeInsets.fromLTRB(18, 30, 0, 0),
                child: CustomTitle(text: 'Задачи', isVisible: false),
              )),
           const Padding(
            padding: EdgeInsets.fromLTRB(8, 10, 8, 0),
            child: SearchBox(),
          ),
          Padding(
            padding: const EdgeInsets.fromLTRB(8, 10, 8, 0),
            child:ButtonBar(
              alignment: MainAxisAlignment.center,
              children: [
                ElevatedButton(
                    onPressed:() {

                      FillOrgerByIsCompleted(false).then((value) => setState(() {}));

                      _firstButtonColor = Color.fromRGBO(173, 38, 185, 1);
                      _secondButtonColor = Color.fromRGBO(227, 228, 232, 1);
                      _firstTextColor = Colors.white;
                      _secondTextColor = Colors.black87;

                      setState(() {});
                    },
                    style: ButtonStyle(
                      shape: MaterialStateProperty.all(RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(15.0)
                      )),
                      backgroundColor: MaterialStateProperty.all(_firstButtonColor),
                      foregroundColor: MaterialStateProperty.all(_firstTextColor),
                      textStyle: MaterialStateProperty.all(
                        const TextStyle(
                        fontSize: 16.0,
                        fontWeight: FontWeight.w500,
                        fontFamily: 'Ubuntu',
                      ),
                      )
                    ),
                    child: const Padding(
                      padding: EdgeInsets.symmetric(vertical: 14.0,horizontal: 24.0),
                      child: Text("В процессе"),
                    )
                ),
                ElevatedButton(
                    onPressed:(){

                      FillOrgerByIsCompleted(true).then((value) => setState(() {}));

                      _secondButtonColor = Color.fromRGBO(173, 38, 185, 1);
                      _firstButtonColor = Color.fromRGBO(227, 228, 232, 1);
                      _secondTextColor = Colors.white;
                      _firstTextColor= Colors.black87;
                      setState(() {});
                      },
                    style: ButtonStyle(
                        shape: MaterialStateProperty.all(RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(15.0)
                        )),
                        backgroundColor: MaterialStateProperty.all(_secondButtonColor),
                        foregroundColor: MaterialStateProperty.all(_secondTextColor),
                        textStyle: MaterialStateProperty.all(
                          const TextStyle(
                            fontSize: 16.0,
                            fontWeight: FontWeight.w500,
                            fontFamily: 'Ubuntu',
                          ),
                        )
                    ),
                    child: const Padding(
                      padding: EdgeInsets.symmetric(vertical: 14.0,horizontal: 6.0),
                      child: Text("Выполненные"),
                    )
                ),
              ],
            ),
          ),
          Expanded(
            child: temp.isEmpty ? Center(child: Text('Нет заметок')) :  ListView.builder(
              shrinkWrap: true,
              key:  Key(temp.hashCode.toString()),
              itemCount: temp.length,
              itemBuilder: (BuildContext context, int index) {
                return NoteCard(note: temp[index]);
            },
          ),
          ),
          IconButton(
              onPressed: (){
                Navigator.pushNamed(context, '/editNote');
              },
              icon: Icon(Icons.add))


        ],
      ),
    );;
  }
}
