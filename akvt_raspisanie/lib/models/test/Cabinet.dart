import 'package:akvt_raspisanie/models/test/Corpus.dart';
import 'package:akvt_raspisanie/models/test/TypeCabinet.dart';


class Cabinet{
  late int id;
  late int num;
  late Corpus corpus;
  late TypeCabinet typeCabinet;

  Cabinet(this.id, this.num, this.corpus, this.typeCabinet);
}