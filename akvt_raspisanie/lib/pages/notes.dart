
import 'package:akvt_raspisanie/DB/DB.dart';
import 'package:akvt_raspisanie/customControl/NoteCard.dart';
import 'package:flutter/material.dart';
import 'package:isar/isar.dart';
import '../customControl/CustomTitle.dart';
import '../customControl/SearchBox.dart';


class Notes extends StatefulWidget {
  const Notes({super.key});

  @override
  State<Notes> createState() => _NotesState();
}

class _NotesState extends State<Notes> {

  // static Note note1 = Note("ббvvvvvvvv гей" ,false, DateTime.now(), "гг просрали");
  // static Note note2 = Note("ббvvvvvvvv гей" ,false, DateTime.now(), "бб лох");
  // static Note note3 = Note("ббvvvvvvvv гей" ,true, DateTime.now(), "бб норм чел");
  // static Note note4 = Note("ббvvvvvvvv гей" ,false, DateTime.now(), "гг просрали");
  // static Note note5 = Note("ббvvvvvvvv гей" ,false, DateTime.now(), "ZZZZZ");
  // static Note note6 = Note("ббvvvvvvvv гей" ,true, DateTime.now(), "Чучмек");
  // static Note note7 = Note("ббvvvvvvvv гей" ,false, DateTime.now(), "гг просрали");
  // static Note note8 = Note("ббvvvvvvvv гей" ,false, DateTime.now(), "бб лох");
  // static Note note9 = Note("ббvvvvvvvv гей" ,true, DateTime.now(), "бб норм чел");

  // List<Note> notes = [note1,note2,note3,note4,note5,note6,note7,note8,note9,];

  List<Note> notes = [];

  late Color _firstButtonColor = Color.fromRGBO(173, 38, 185, 1);
  late Color _secondButtonColor = Color.fromRGBO(227, 228, 232, 1);
  late Color _firstTextColor = Colors.white;
  late Color _secondTextColor = Colors.black87;

  Future<void> FillOrgerByIsCompleted (bool b) async{
    final isar = await AppDB.isar;
      notes = await isar.notes.filter().isCompletedEqualTo(b).findAll();
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
            Padding(
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
                      print(notes.length);

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
                      print(notes.length);

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
            child: notes.isEmpty ? Center(child: Text('Нет заметок')) :  ListView.builder(
              shrinkWrap: true,
              key:  Key(notes.hashCode.toString()),
              itemCount: notes.length,
              itemBuilder: (BuildContext context, int index) {
                return NoteCard(note: notes[index]);
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
