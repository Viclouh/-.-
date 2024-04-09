import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../DB/DB.dart';

class NoteCard extends StatefulWidget {
  Note note;

  NoteCard({super.key,required this.note,});

  @override
  State<NoteCard> createState() => _NoteCardState(this.note);
}

class _NoteCardState extends State<NoteCard> {

  Note note;

  _NoteCardState(this.note);

  Future<void> ChangeNoteCompleted(bool b) async{
    final isar = await AppDB.isar;
    note = Note.Full( note.id,note.name , b, note.dateTime, note.description);
    await isar.writeTxn(() async {
      isar.notes.put(note);
    });
  }
  Future<void> DeleteNote() async{
    final isar = await AppDB.isar;
    await isar.writeTxn(() async {
      isar.notes.delete(note.id);
    });
  }

  @override
  Widget build(BuildContext context) {
    return  Padding(
        padding: EdgeInsets.fromLTRB(8, 0, 8, 8),
      child: Container(
          decoration: BoxDecoration(
              color: const Color.fromRGBO(243, 243, 243, 1),
              border: Border.all(style: BorderStyle.none),
              borderRadius: const BorderRadius.all(Radius.circular(16.0))),
            width: MediaQuery.of(context).size.width,
              child:  Row(
                children: [
                  Checkbox(
                      checkColor: Colors.black87,
                      fillColor: MaterialStateProperty.all(Colors.transparent),
                      shape: CircleBorder(),
                      value: note.isCompleted,
                      onChanged: (bool? value){
                        ChangeNoteCompleted(value!).then((value) => setState(() {}));
                      }),
                    Expanded(
                        child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Padding(
                          padding: EdgeInsets.fromLTRB(8,8,8,0),
                          child: Text(note.name,
                              overflow: TextOverflow.ellipsis,
                              style: const TextStyle(
                                  fontSize: 18.0,
                                  fontFamily: 'Ubuntu',
                                  color: Colors.black,
                                  fontWeight: FontWeight.w500)),
                        ),
                        Padding(
                            padding: EdgeInsets.fromLTRB(8,8,8,0),
                          child: Text(note.description,
                              overflow: TextOverflow.ellipsis,
                              style: const TextStyle(
                                  fontSize: 16.0,
                                  fontFamily: 'Ubuntu',
                                  color: Colors.black87,
                                  fontWeight: FontWeight.w500)
                          ),
                        ),
                        Padding(
                          padding: EdgeInsets.fromLTRB(8,8,8,0),
                          child: Text('${DateFormat.yMMMMEEEEd('ru').format(note.dateTime)}, ${DateFormat.Hm().format(note.dateTime)}',
                              style: const TextStyle(
                                  fontSize: 16.0,
                                  fontFamily: 'Ubuntu',
                                  color: Colors.black87,
                                  fontWeight: FontWeight.w500)),
                        )
                      ],
                    )
                    ),
                  IconButton(
                          onPressed: (){
                            DeleteNote().then((value) => setState(() {}));
                          },
                          icon: const Icon(Icons.delete)
                  )

                ],
              ),
            )
    );
  }
}
