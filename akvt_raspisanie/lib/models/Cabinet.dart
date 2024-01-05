import 'package:akvt_raspisanie/models/TypeCabinet.dart';
import 'package:akvt_raspisanie/models/Corpus.dart';

class Cabinet{
  late int id;
  late int num;
  late Corpus corpus;
  late TypeCabinet typeCabinet;

  Cabinet(this.id, this.num, this.corpus, this.typeCabinet);
}