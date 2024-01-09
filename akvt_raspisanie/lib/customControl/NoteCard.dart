import 'package:flutter/material.dart';
import 'package:flutter_localization/flutter_localization.dart';
import 'package:intl/intl.dart';

import '../DB/Notes.dart';
import '../models/Note.dart';
import '../models/test/Para.dart';

class NoteCard extends StatefulWidget {
  TODO note;

  NoteCard({super.key,required this.note,});

  @override
  State<NoteCard> createState() => _NoteCardState(this.note);
}

class _NoteCardState extends State<NoteCard> {

  TODO note;

  _NoteCardState(this.note);

  Future<void> ChangeTODOComplete(bool b) async{

    final database = AppDatabase();
    note = TODO(
        id:note.id,
        name: note.name,
        isCompleted: b,
        datetime: note.datetime,
        description: note.description);

    database.update(database.tODOs).replace(note);

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
                        ChangeTODOComplete(value!).then((value) => setState(() {}));
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
                          child: Text('${DateFormat.yMMMMEEEEd('ru').format(note.datetime)}, ${DateFormat.Hm().format(note.datetime)}',
                              style: const TextStyle(
                                  fontSize: 16.0,
                                  fontFamily: 'Ubuntu',
                                  color: Colors.black87,
                                  fontWeight: FontWeight.w500)),
                        )
                      ],
                    )
                    )

                ],
              ),
            )
    );
  }
}
