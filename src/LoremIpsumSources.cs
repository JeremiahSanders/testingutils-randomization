using System.Text.RegularExpressions;

namespace Jds.TestingUtils.Randomization;

internal static class LoremIpsumSources
{
  public enum LoremIpsumSource
  {
    CiceroDeFinibusBonorumEtMalorum,
    MeditatioIvDeVeroEtFalso,
    MeditatioViDeRerumMaterialiumExistentiaEtRealiMentisACorporeDistinctione
  }

  public const string CiceroDeFinibusBonorumEtMalorum = @"
Certe inquam pertinax non ero tibique si mihi probabis ea quae dices libenter assentiar.
Probabo inquit modo ista sis aequitate quam ostendis.
sed uti oratione perpetua malo quam interrogare aut interrogari.
Ut placet inquam.
Tum dicere exorsus est.
Primum igitur inquit sic agam ut ipsi auctori huius disciplinae placet constituam quid et quale sit id de quo quaerimus non quo ignorare vos arbitrer sed ut ratione et via procedat oratio.
quaerimus igitur quid sit extremum et ultimum bonorum quod omnium philosophorum sententia tale debet esse ut ad id omnia referri oporteat ipsum autem nusquam.
hoc Epicurus in voluptate ponit quod summum bonum esse vult summumque malum dolorem idque instituit docere sic Omne animal simul atque natum sit voluptatem appetere eaque gaudere ut summo bono dolorem aspernari ut summum malum et quantum possit a se repellere idque facere nondum depravatum ipsa natura incorrupte atque integre iudicante.
itaque negat opus esse ratione neque disputatione quam ob rem voluptas expetenda fugiendus dolor sit.
sentiri haec putat ut calere ignem nivem esse albam dulce mel.
quorum nihil oportere exquisitis rationibus confirmare tantum satis esse admonere.
interesse enim inter argumentum conclusionemque rationis et inter mediocrem animadversionem atque admonitionem.
altera occulta quaedam et quasi involuta aperiri altera prompta et aperta iudicari.
etenim quoniam detractis de homine sensibus reliqui nihil est necesse est quid aut ad naturam aut contra sit a natura ipsa iudicari.
ea quid percipit aut quid iudicat quo aut petat aut fugiat aliquid praeter voluptatem et dolorem? Sunt autem quidam e nostris qui haec subtilius velint tradere et negent satis esse quid bonum sit aut quid malum sensu iudicari sed animo etiam ac ratione intellegi posse et voluptatem ipsam per se esse expetendam et dolorem ipsum per se esse fugiendum.
itaque aiunt hanc quasi naturalem atque insitam in animis nostris inesse notionem ut alterum esse appetendum alterum aspernandum sentiamus.
Alii autem quibus ego assentior cum a philosophis compluribus permulta dicantur cur nec voluptas in bonis sit numeranda nec in malis dolor non existimant oportere nimium nos causae confidere sed et argumentandum et accurate disserendum et rationibus conquisitis de voluptate et dolore disputandum putant.
Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium totam rem aperiam eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.
nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt neque porro quisquam est qui dolorem ipsum quia dolor sit amet consectetur adipisci velit sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.
ut enim ad minima veniam quis nostrum exercitationem ullam corporis suscipit laboriosam nisi ut aliquid ex ea commodi consequatur?
Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?
At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint obcaecati cupiditate non provident similique sunt in culpa qui officia deserunt mollitia animi id est laborum et dolorum fuga.
et harum quidem rerum facilis est et expedita distinctio.
nam libero tempore cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus omnis voluptas assumenda est omnis dolor repellendus.
temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae.
itaque earum rerum hic tenetur a sapiente delectus ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.";

  public const string MeditatioIvDeVeroEtFalso = @"
  Ita me his diebus assuefeci in mente a sensibus abducenda, tamque
  accurate animadverti perpauca esse quae de rebus corporeis vere
  percipiantur, multoque plura de mente humana, multo adhuc plura de Deo
  cognosci, ut jam absque ulla difficultate cogitationem a rebus
  imaginabilibus ad intelligibiles tantum, atque ab omni materia secretas
  convertam: et sane multo magis distinctam habeo ideam mentis humanae,
  quatenus est res cogitans, non extensa in longum, latum, et
  profundum, nec aliud quid a corpore habens, quam ideam ullius rei
  corporeae: cumque attendo me dubitare, sive esse rem incompletam et
  dependentem, adeo clara et distincta idea entis independentis et
  completi, hoc est Dei, mihi occurrit; et ex hoc uno, quod talis idea in
  me sit, sive quod ego ideam illam habens existam, adeo manifeste
  concludo Deum etiam existere, atque ab illo singulis momentis totam
  existentiam meam dependere, ut nihil evidentius, nihil certius ab humano
  ingenio cognosci posse confidam. Jamque videre videor aliquam viam per
  quam ab ista contemplatione veri Dei, in quo nempe sunt omnes thesauri
  scientiarum et sapientiae absconditi, ad caeterarum rerum cognitionem
  deveniatur.
  
  In primis enim agnosco fieri non posse ut ille me unquam fallat; in omni
  enim fallacia vel deceptione aliquid imperfectionis reperitur; et
  quamvis posse fallere nonnullum esse videatur acuminis, aut potentiae
  argumentum, proculdubio velle fallere vel malitiam vel
  imbecillitatem testatur, nec proinde in Deum cadit.
  
  Deinde experior quandam in me esse judicandi facultatem, quam certe, ut
  et reliqua omnia quae in me sunt, a Deo accepi, cumque ille nolit me
  fallere, talem profecto non dedit, ut, dum ea recte utor, possim unquam
  errare.
  
  Nec ullum de hac re dubium superesset, nisi inde sequi videretur, me
  igitur errare nunquam posse, nam si quodcunque in me est, a Deo habeo,
  nec ullam ille mihi dederit errandi facultatem, non videor posse unquam
  errare. Atque ita prorsus, quamdiu de Deo tantum cogito, totusque in eum
  me converto, nullam erroris, aut falsitatis causam deprehendo; sed
  postmodum ad me reversus experior me tamen innumeris erroribus esse
  obnoxium, quorum causam inquirens animadverto non tantum Dei, sive entis
  summe perfecti realem et positivam, sed etiam, ut ita loquar, nihili,
  sive ejus quod ab omni perfectione summe abest, negativam quandam ideam
  mihi obversari, et me tanquam medium quid inter Deum et nihil, sive
  inter summum ens et non ens ita esse constitutum, ut, quantenus a
  summo ente sum creatus, nihil quidem in me sit, per quod fallar, aut in
  errorem inducar, sed quatenus etiam quodammodo de nihilo, sive de non
  ente participo, hoc est quatenus non sum ipse summum ens, desuntque mihi
  quam plurima, non adeo mirum esse quod fallar: atque ita certe intelligo
  errorem, quatenus error est, non esse quid reale quod a Deo dependeat,
  sed tantummodo esse defectum, nec proinde ad errandum mihi opus esse
  aliqua facultate in hunc finem a Deo tributa, sed contingere ut errem ex
  eo quod facultas verum judicandi quam ab illo habeo, non sit in me
  infinita.
  
  Verumtamen hoc nondum omnino satisfacit, non enim error est pura
  negatio, sed privatio, sive carentia cujusdam cognitionis, quae in me
  quodammodo esse deberet; atque attendenti ad Dei naturam non videtur
  fieri posse, ut ille aliquam in me posuerit facultatem quae non sit in
  suo genere perfecta, sive quae aliqua sibi debita perfectione sit
  privata; nam si quo  peritior est artifex, eo perfectiora opera ab
  illo proficiscantur, quid potest a summo illo rerum omnium conditore
  factum esse quod non sit omnibus numeris absolutum? nec dubium est quin
  potuerit Deus me talem creare ut nunquam fallerer; nec etiam dubium est
  quin velit semper id quod est optimum; anne ergo melius est me falli
  quam non falli?
  
  Dum haec perpendo attentius, occurrit primo non mihi esse mirandum si
  quaedam a Deo fiant quorum rationes non intelligam; nec de ejus
  existentia ideo esse dubitandum, quod forte quaedam alia esse experiar
  quae quare, vel quomodo ab illo facta sint non comprehendo; cum enim jam
  sciam naturam meam esse valde infirmam et limitatam, Dei autem naturam
  esse immensam, incomprehensibilem, infinitam, ex hoc satis etiam scio
  innumerabilia illum posse quorum causas ignorem; atque ob hanc unicam
  rationem totum illud causarum genus quod a fine peti solet in rebus
  Physicis nullum usum habere existimo; non enim absque temeritate
  me puto posse investigare fines Dei.
  
  Occurrit etiam non unam aliquam creaturam separatim, sed omnem rerum
  universitatem esse spectandam, quoties an opera Dei perfecta sint
  inquirimus: quod enim forte non immerito, si solum esset, valde
  imperfectum videretur, ut habens in mundo rationem partis est
  perfectissimum; et quamvis ex quo de omnibus volui dubitare nihil adhuc
  praeter me, et Deum existere certo cognovi, non possum tamen, ex quo
  immensam Dei potentiam animadverti, negare quin multa alia ab illo facta
  sint, vel saltem fieri possint, adeo ut ego rationem partis in rerum
  universitate obtineam.
  
  Deinde, ad me propius accedens, et qualesnam sint errores mei qui soli
  imperfectionem aliquam in me arguunt investigans, adverto illos a
  duabus causis simul concurrentibus dependere, nempe a facultate
  cognoscendi quae in me est, et a facultate eligendi sive ab arbitrii
  libertate, hoc est ab intellectu, et simul a voluntate. Nam per solum
  intellectum percipio tantum ideas de quibus judicium ferre
  possum, nec ullus error proprie dictus in eo praecise sic spectato
  reperitur; quamvis enim innumerae fortasse res existant, quarum ideae
  nullae in me sunt, non tamen proprie illis privatus, sed negative tantum
  destitutus sum dicendus, quia nempe rationem nullam possum afferre, qua
  probem Deum mihi majorem quam dederit cognoscendi facultatem dare
  debuisse; atque quantumvis peritum artificem esse intelligam, non tamen
  ideo puto illum in singulis ex suis operibus omnes perfectiones ponere
  debuisse quas in aliquibus ponere potest. Nec vero etiam queri possum
  quod non satis amplam et perfectam voluntatem, sive arbitrii libertatem
  a Deo acceperim, nam sane nullis illam limitibus circumscribi experior.
  Et quod valde notandum mihi videtur, nulla alia in me sunt tam perfecta,
  aut tanta, quin intelligam perfectiora, sive majora adhuc esse posse.
  Nam si, exempli causa, facultatem intelligendi considero, statim agnosco
  perexiguam illam et valde finitam in me esse, simulque alterius
  cujusdam multo majoris, imo maximae, atque infinitae ideam formo,
  illamque ex hoc ipso quod ejus ideam formare possim, ad Dei naturam
  pertinere percipio. Eadem ratione si facultatem recordandi, vel
  imaginandi, vel quaslibet alias examinem, nullam plane invenio, quam non
  in me tenuem, et circumscriptam, in Deo immensam esse intelligam. Sola
  est voluntas, sive arbitrii libertas, quam tantam in me experior ut
  nullius majoris ideam apprehendam; adeo ut illa praecipue sit, ratione
  cujus imaginem quandam, et similitudinem Dei me referre intelligo: nam
  quamvis major absque comparatione in Deo quam in me sit, tum ratione
  cognitionis et potentiae quae illi adjunctae sunt, redduntque ipsam
  magis firmam et efficacem; tum ratione objecti, quoniam ad plura se
  extendit; non tamen in se formaliter et praecise spectata major videtur,
  quia tantum in eo consistit quod idem vel facere, vel non facere hoc
  est affirmare vel negare, prosequi vel fugere possimus, vel potius in
  eo tantum quod ad id quod nobis ab intellectu proponitur
  affirmandum vel negandum, sive prosequendum vel fugiendum ita feramur,
  ut a nulla vi externa nos ad id determinari sentiamus. Neque enim opus
  est me in utramque partem ferri posse ut sim liber, sed contra quo magis
  in unam propendeo, sive quia rationem veri et boni in ea evidenter
  intelligo, sive quia Deus intima cogitationis meae ita disponit, tanto
  liberius illam eligo; nec sane divina gratia, nec naturalis cognitio
  unquam imminuunt libertatem, sed potius augent, et corroborant.
  Indifferentia autem illa quam experior, cum nulla me ratio in unam
  partem magis quam in alteram impellit, est infimus gradus libertatis, et
  nullam in ea perfectionem, sed tantummodo in cognitione defectum,
  sive negationem quandam testatur; nam si semper quid verum et bonum sit
  clare viderem, nunquam de eo quod esset judicandum vel eligendum
  deliberarem, atque ita, quamvis plane liber, nunquam tamen indifferens
  esse possem.
  
  Ex his autem percipio nec vim volendi, quam a Deo habeo, per se
  spectatam causam esse errorum meorum; est enim amplissima, atque in suo
  genere perfecta; neque etiam vim intelligendi, nam quidquid intelligo,
  cum a Deo habeam ut intelligam, procul dubio recte intelligo, nec in eo
  fieri potest ut fallar. Unde ergo nascuntur mei errores? nempe ex hoc
  uno quod cum latius pateat voluntas quam intellectus, illam non intra
  eosdem limites contineo, sed etiam ad illa quae non intelligo extendo;
  ad quae cum sit indifferens, facile a vero et bono deflectit, atque ita
  et fallor et pecco.
  
  Exempli causa, cum examinarem hisce diebus an aliquid in mundo
  existeret, atque adverterem, ex hoc ipso quod illud examinarem,
  evidenter sequi me existere, non potui quidem non judicare illud quod
  tam clare intelligebam verum esse, non quod ab aliqua vi externa fuerim
  ad id coactus, sed quia ex magna luce in intellectu magna consequuta est
  propensio in voluntate, atque ita tanto magis sponte et libere illud
  credidi, quanto minus fui ad istud ipsum  indifferens. Nunc autem,
  non tantum scio me, quatenus sum res quaedam cogitans, existere, sed
  praeterea etiam idea quaedam naturae corporeae mihi obversatur,
  contingitque ut dubitem an natura cogitans quae in me est, vel potius
  quae ego ipse sum, alia sit ab ista natura corporea, vel an ambae
  idem sint; et suppono nullam adhuc intellectui meo rationem occurrere,
  quae mihi unum magis quam aliud persuadeat, certe ex hoc ipso sum
  indifferens ad utrumlibet affirmandum vel negandum, vel etiam ad nihil
  de ea re judicandum.
  
  Quinimo etiam haec indifferentia non ad ea tantum se extendit, de quibus
  intellectus nihil plane cognoscit, sed generaliter ad omnia quae ab illo
  non satis perspicue cognoscuntur eo ipso tempore, quo de iis a voluntate
  deliberatur; quantumvis enim probabiles conjecturae me trahant in unam
  partem, sola cognitio quod sint tantum conjecturae, non autem certae
  atque indubitabiles rationes, sufficit ad assensionem meam in contrarium
  impellendam. Quod satis  his diebus sum expertus, cum illa omnia
  quae prius ut vera quam maxime credideram, propter hoc unum quod de iis
  aliquo modo posse dubitari deprehendissem, plane falsa esse supposui.
  
  Cum autem quid verum sit, non satis clare et distincte percipio, si
  quidem a judicio ferendo abstineam, clarum est me recte agere, et non
  falli, sed si vel affirmem, vel negem, tunc libertate arbitrii non recte
  utor; atque si in eam partem quae falsa est me convertam, plane
  fallar; si vero alteram amplectar, casu quidem incidam in
  veritatem, sed non ideo culpa carebo, quia lumine naturali manifestum
  est perceptionem intellectus praecedere semper debere voluntatis
  determinationem; atque in hoc liberi arbitrii non recto usu privatio
  illa inest quae formam erroris constituit, privatio, inquam, inest in
  ipsa operatione, quatenus a me procedit, sed non in facultate quam a Deo
  accepi, nec etiam in operatione quatenus ab illo dependet.
  
  Neque enim habeo causam ullam conquerendi, quod Deus mihi non
  majorem vim intelligendi, sive non majus lumen naturale dederit
  quam dedit, quia est de ratione intellectus finiti ut multa non
  intelligat, et de ratione intellectus creati ut sit finitus, estque quod
  agam gratias illi qui mihi nunquam quicquam debuit pro eo quod largitus
  est, non autem quod putem me ab illo iis esse privatum, sive illum mihi
  ea abstulisse quae non dedit.
  
  Non habeo etiam causam conquerendi quod voluntatem dederit latius
  patentem quam intellectum, cum enim voluntas in una tantum re, et
  tanquam in indivisibili consistat, non videtur ferre ejus natura ut
  quicquam ab illa demi possit; et sane quo amplior est, tanto majores
  debeo gratias ejus datori.
  
  Nec denique etiam queri debeo, quod Deus mecum concurrat ad eliciendos
  illos actus voluntatis, sive illa judicia in quibus fallor: illi enim
  actus sunt omnino veri et boni, quatenus a Deo dependent, et major in me
  quodammodo perfectio est quod illos possim elicere quam si non possem.
  Privatio autem, in qua sola ratio formalis falsitatis et culpae
  consistit, nullo Dei concursu indiget, quia non est res, neque ad illum
  relata ut causam privatio, sed tantummodo negatio dici debet. Nam sane
  nulla imperfectio in Deo est, quod mihi libertatem dederit assentiendi
  vel non assentiendi quibusdam, quorum claram et distinctam perceptionem
  in intellectu meo non posuit, sed proculdubio in me imperfectio est,
  quod ista libertate non bene utar, et de iis quae non recte intelligo
  judicium feram. Video tamen fieri a Deo facile potuisse ut, etiamsi
  manerem liber, et cognitionis finitae, nunquam tamen errarem, nempe si
  vel intellectui meo claram et distinctam perceptionem omnium de quibus
  unquam essem deliberaturus indidisset; vel tantum si adeo firmiter
  memoriae impressisset, de nulla unquam re esse judicandum quam clare et
  distincte non intelligerem, ut nunquam ejus possem oblivisci; et facile
  intelligo me, quatenus rationem habeo totius cujusdam, perfectiorem
  futurum fuisse, quam nunc sum, si talis a Deo factus essem. Sed non ideo
   possum negare quin major quodammodo perfectio sit in tota rerum
  universitate quod quaedam ejus partes ab erroribus immunes non sint,
  aliae vero sint, quam si omnes plane similes essent, et nullum habeo jus
  conquerendi quod eam me Deus in mundo personam sustinere voluerit quae
  non est omnium praecipua, et maxime perfecta.
  
  Ac praeterea, etiam ut non possim ab erroribus abstinere, priori illo
  modo qui pendet ab evidenti eorum omnium perceptione, de quibus est
  deliberandum, possum tamen illo altero qui pendet ab eo tantum quod
  recorder, quoties de rei veritate non liquet, a judicio ferendo esse
  abstinendum; nam quamvis eam in me infirmitatem esse experiar, ut non
  possim semper uni et eidem cognitioni defixus inhaerere, possum tamen
  attenta et saepius iterata meditatione efficere ut ejusdem, quoties usus
  exiget, recorder, atque ita habitum quemdam non errandi acquiram.
  
  Qua in re cum maxima et praecipua hominis perfectio consistat, non parum
  me hodierna meditatione lucratum esse  existimo, quod erroris et
  falsitatis causam investigarim: et sane nulla alia esse potest ab ea
  quam explicui, nam quoties voluntatem in judiciis ferendis ita contineo,
  ut ad ea tantum se extendat quae illi clare et distincte ab intellectu
  exhibentur, fieri plane non potest ut errem, quia omnis clara et
  distincta perceptio proculdubio est aliquid, ac proinde a nihilo
  esse non potest, sed necessario Deum authorem habet, Deum inquam illum
  summe perfectum, quem fallacem esse repugnat, ideoque proculdubio est
  vera. Nec hodie tantum didici quid mihi sit cavendum ut nunquam fallar,
  sed simul etiam quid agendum ut assequar veritatem; assequar enim illam
  profecto, si tantum ad omnia quae perfecte intelligo satis attendam,
  atque illa a reliquis quae confusius et obscurius apprehendo, secernam.
  Cui rei diligenter imposterum operam dabo.
 ";

  public const string MeditatioViDeRerumMaterialiumExistentiaEtRealiMentisACorporeDistinctione = @"
Reliquum est ut examinem an res materiales existant et quidem jam ad
minimum scio illas quatenus sunt purae Matheseos objectum posse
existere quandoquidem ipsas clare et distincte percipio. Non enim
dubium est quin Deus sit capax ea omnia efficiendi quae ego sic 
percipiendi sum capax. nihilque unquam ab illo fieri non posse judicavi
nisi propter hoc quod illud a me distincte percipi repugnaret. Praeterea
ex imaginandi facultate qua me uti experior dum circa res istas
materiales versor sequi videtur illas existere nam attentius
consideranti quidnam sit imaginatio nihil aliud esse apparet quam
quaedam applicatio facultatis cognoscitivae ad corpus ipsi intime
praesens ac proinde existens.

Quod ut planum fiat primo examino differentiam quae est inter
imaginationem et puram intellectionem. Nempe exempli causa cum
triangulum imaginor non tantum intelligo illud esse figuram tribus
lineis comprehensam sed simul etiam istas tres lineas tanquam
praesentes acie mentis intueor atque hoc est quod imaginari appello. Si
vero de chiliogono velim cogitare equidem aeque bene intelligo illud
esse figuram constantem mille lateribus ac intelligo triangulum esse
figuram constantem tribus sed non eodem modo illa mille latera 
imaginor sive tanquam praesentia intueor et quamvis tunc propter
consuetudinem aliquid semper imaginandi quoties de re corporea cogito
figuram forte aliquam confuse mihi repraesentem patet tamen illam non
esse chiliogonum quia nulla in re est diversa ab ea quam mihi etiam
repraesentarem si de myriogono aliave quavis figura plurimorum laterum
cogitarem. nec quicquam juvat ad eas proprietates quibus chiliogonum ab
aliis polygonis differt agnoscendas. Si vero de pentagono quaestio sit
possum quidem ejus figuram intelligere sicut figuram chiliogoni absque
ope imaginationis. sed possum etiam eandem imaginari applicando
scilicet aciem mentis ad ejus quinque latera simulque ad aream iis
contentam. et manifeste hic animadverto mihi peculiari quadam animi
contentione opus esse ad imaginandum. qua non utor ad intelligendum
quae nova animi contentio differentiam inter imaginationem et
intellectionem puram clare ostendit. 

Ad haec considero istam vim imaginandi quae in me est prout differt a
vi intelligendi ad mei ipsius hoc est ad mentis meae essentiam non
requiri. nam quamvis illa a me abesset procul dubio manerem nihilominus
ille idem qui nunc sum. unde sequi videtur illam ab aliqua re a me
diversa pendere. atque facile intelligo si corpus aliquod existat cui
mens sit ita conjuncta ut ad illud veluti inspiciendum pro arbitrio se
applicet fieri posse ut per hoc ipsum res corporeas imaginer. adeo ut
hic modus cogitandi in eo tantum a pura intellectione differat quod
mens dum intelligit se ad seipsam quodammodo convertat respiciatque
aliquam ex ideis quae illi ipsi insunt. dum autem imaginatur se
convertat ad corpus et aliquid in eo ideae vel a se intellectae vel
sensu perceptae conforme intueatur. Facile inquam intelligo
imaginationem ita perfici posse siquidem corpus existat. et quia nullus
alius modus aeque conveniens occurrit ad illam explicandam probabiliter
inde conjicio corpus existere. sed probabiliter tantum et 
quamvis accurate omnia investigem nondum tamen video ex ea naturae
corporeae idea distincta quam in imaginatione mea invenio ullum sumi
posse argumentum quod necessario concludat aliquod corpus existere.

Soleo vero alia multa imaginari praeter illam naturam corpoream quae
est purae Matheseos objectum ut colores sonos sapores dolorem et
similia sed nulla tam distincte. et quia haec percipio melius sensu a
quo videntur ope memoriae ad imaginationem pervenisse. ut commodius de
ipsis agam eadem opera etiam de sensu est agendum videndumque an ex
iis quae isto cogitandi modo quem sensum appello percipiuntur certum
aliquod argumentum pro rerum corporearum existentia habere possim.

Et primo quidem apud me hic repetam quaenam illa sint quae antehac ut
sensu percepta vera esse putavi et quas ob causas id putavi. deinde
etiam causas expendam propter quas eadem postea in dubium revocavi. ac
denique considerabo quid mihi nunc de iisdem sit credendum. 

Primo itaque sensi me habere caput manus pedes et membra caetera ex
quibus constat illud corpus quod tanquam mei partem vel forte etiam
tanquam me totum spectabam. sensique hoc corpus inter alia multa corpora
versari a quibus variis commodis vel incommodis affici potest et
commoda ista sensu quodam voluptatis et incommoda sensu doloris
metiebar. Atque praeter dolorem et voluptatem sentiebam etiam in me
famem sitim aliosque ejusmodi appetitus. itemque corporeas quasdam
propensiones ad hilaritatem ad tristitiam ad iram similesque alios
affectus. foris vero praeter corporum extensionem et figuras et
motus sentiebam etiam in illis duritiem et calorem aliasque tactiles
qualitates. ac praeterea lumen et colores et odores et sapores et
sonos ex quorum varietate caelum terram maria et reliqua corpora ab
invicem distinguebam. Nec sane absque ratione ob ideas istarum omnium
qualitatum quae cogitationi meae se offerebant et quas solas 
proprie et immediate sentiebam putabam me sentire res quasdam a mea
cogitatione plane diversas nempe corpora a quibus ideae istae
procederent. experiebar enim illas absque ullo meo consensu mihi
advenire adeo ut neque possem objectum ullum sentire quamvis vellem
nisi illud sensus organo esset praesens. nec possem non sentire cum erat
praesens. cumque ideae sensu perceptae essent multo magis vividae et
expressae et suo etiam modo magis distinctae quam ullae ex iis quas
ipse prudens et sciens meditando effingebam vel memoriae meae impressas
advertebam fieri non posse videbatur ut a meipso procederent. ideoque
supererat ut ab aliis quibusdam rebus advenirent. Quarum rerum cum
nullam aliunde notitiam haberem quam ex istis ipsis ideis non poterat
aliud mihi venire in mentem quam illas iis similes esse. Atque etiam
quia recordabar me prius usum fuisse sensibus quam ratione videbamque
ideas quas ipse effingebam non tam expressas esse quam illae 
erant quas sensu percipiebam et plerumque ex earum partibus componi
facile mihi persuadebam nullam plane me habere in intellectu quam non
prius habuissem in sensu. Non etiam sine ratione corpus illud quod
speciali quodam jure meum appellabam magis ad me pertinere quam alia
ulla arbitrabar. neque enim ab illo poteram unquam sejungi ut a
reliquis. omnes appetitus et affectus in illo et pro illo sentiebam. ac
denique dolorem et titillationem voluptatis in ejus partibus non autem
in aliis extra illud positis advertebam. Cur vero ex isto nescio quo
doloris sensu quaedam animi tristitia et ex sensu titillationis
laetitia quaedam consequatur curve illa nescio quae vellicatio
ventriculi quam famem voco me de cibo sumendo admoneat gutturis vero
ariditas de potu et ita de caeteris non aliam sane habebam rationem
nisi quia ita doctus sum a natura. neque enim ulla plane est affinitas
saltem quam ego intelligam inter istam vellicationem et cibi sumendi
voluntatem sive inter sensum rei dolorem inferentis et 
cogitationem tristitiae ab isto sensu exortae. Sed et reliqua omnia quae
de sensuum objectis judicabam videbar a natura didicisse prius enim
illa ita se habere mihi persuaseram quam rationes ullas quibus hoc
ipsum probaretur expendissem.

Postea vero multa paulatim experimenta fidem omnem quam sensibus
habueram labefactarunt nam et interdum turres quae rotundae visae
fuerant e longinquo quadratae apparebant e propinquo et statuae
permagnae in earum fastigiis stantes non magnae e terra spectanti
videbantur. et talibus aliis innumeris in rebus sensuum externorum
judicia falli deprehendebam. nec externorum duntaxat sed etiam
internorum nam quid dolore intimius esse potest atqui audiveram
aliquando ab iis quibus crus aut brachium fuerat abscissum se sibi
videri adhuc interdum dolorem sentire in ea parte corporis qua carebant.
ideoque etiam in me non plane certum esse videbatur membrum aliquod mihi
dolere quamvis sentirem in eo dolorem. Quibus etiam duas maxime 
generales dubitandi causas nuper adjeci prima erat quod nulla unquam
dum vigilo me sentire crediderim quae non etiam inter dormiendum possim
aliquando putare me sentire. cumque illa quae sentire mihi videor in
somnis non credam a rebus extra me positis mihi advenire non
advertebam quare id potius crederem de iis quae sentire mihi videor
vigilando. Altera erat quod cum authorem meae originis adhuc ignorarem
vel saltem ignorare me fingerem nihil videbam obstare quo minus essem
natura ita constitutus ut fallerer etiam in iis quae mihi verissima
apparebant. Et quantum ad rationes quibus antea rerum sensibilium
veritatem mihi persuaseram non difficulter ad illas respondebam. Cum
enim viderer ad multa impelli a natura quae ratio dissuadebat non
multum fidendum esse putabam iis quae a natura docentur. Et quamvis
sensuum perceptiones a voluntate mea non penderent non ideo
concludendum esse putabam illas a rebus a me diversis procedere 
quia forte aliqua esse potest in meipso facultas etsi mihi nondum
cognita illarum effectrix.

Nunc autem postquam incipio meipsum meaeque authorem originis melius
nosse non quidem omnia quae habere videor a sensibus puto esse temere
admittenda. sed neque etiam omnia in dubium revocanda.

Et primo quoniam scio omnia quae clare et distincte intelligo talia a
Deo fieri posse qualia illa intelligo satis est quod possim unam rem
absque altera clare et distincte intelligere ut certus sim unam ab
altera esse diversam quia potest saltem a Deo seorsim poni. et non
refert a qua potentia id fiat ut diversa existimetur. ac proinde ex
hoc ipso quod sciam me existere quodque interim nihil plane aliud ad
naturam sive essentiam meam pertinere animadvertam praeter hoc solum
quod sim res cogitans recte concludo meam essentiam in hoc uno
consistere quod sim res cogitans. Et quamvis fortasse vel potius ut
postmodum dicam pro certo habeam corpus quod mihi valde arcte 
conjunctum est quia tamen ex una parte claram et distinctam habeo ideam
mei ipsius quatenus sum tantum res cogitans non extensa. et ex alia
parte distinctam ideam corporis quatenus est tantum res extensa non
cogitans certum est me a corpore meo revera esse distinctum et absque
illo posse existere.

Praeterea invenio in me facultates specialibus quibusdam modis
cogitandi puta facultates imaginandi et sentiendi sine quibus
totum me possum clare et distincte intelligere sed non vice versa illas
sine me hoc est sine substantia intelligente cui insint intellectionem
enim nonnullam in suo formali conceptu includunt unde percipio illas
a me ut modos a re distingui. Agnosco etiam quasdam alias facultates
ut locum mutandi varias figuras induendi et similes quae quidem non
magis quam praecedentes absque aliqua substantia cui insint possunt
intelligi nec proinde etiam absque illa existere sed manifestum 
est has siquidem existant inesse debere substantiae corporeae sive
extensae non autem intelligenti quia nempe aliqua extensio non autem
ulla plane intellectio in earum claro et distincto conceptu continetur.
Jam vero est quidem in me passiva quaedam facultas sentiendi sive ideas
rerum sensibilium recipiendi et cognoscendi sed ejus nullum usum habere
possem nisi quaedam activa etiam existeret sive in me sive in alio
facultas istas ideas producendi vel efficiendi. Atque haec sane in me
ipso esse non potest quia nullam plane intellectionem praesupponit et
me non cooperante sed saepe etiam invito ideae istae producuntur ergo
superest ut sit in aliqua substantia a me diversa in qua quoniam omnis
realitas vel formaliter vel eminenter inesse debet quae est objective
in ideis ab ista facultate productis ut jam supra animadverti vel
haec substantia est corpus sive natura corporea in qua nempe omnia
formaliter continentur quae in ideis objective. vel certe Deus est vel
aliqua creatura corpore nobilior in qua continentur eminenter. 
Atqui cum Deus non sit fallax omnino manifestum est illum nec per se
immediate istas ideas mihi immittere nec etiam mediante aliqua
creatura in qua earum realitas objectiva non formaliter sed eminenter
tantum contineatur. Cum enim nullam plane facultatem mihi dederit ad hoc
agnoscendum sed contra magnam propensionem ad credendum illas a rebus
corporeis emitti non video qua ratione posset intelligi ipsum non esse
fallacem si aliunde quam a rebus corporeis emitterentur Ac proinde res
corporeae existunt. Non tamen forte omnes tales omnino existunt quales
illas sensu comprehendo. quoniam ista sensuum comprehensio in multis
valde obscura est et confusa. sed saltem illa omnia in iis sunt quae
clare et distincte intelligo id est omnia generaliter spectata quae in
purae Matheseos objecto comprehenduntur.

Quantum autem attinet ad reliqua quae vel tantum particularia sunt ut
quod sol sit talis magnitudinis aut figurae etc. vel minus clare
intellecta ut lumen sonus dolor et similia quamvis valde dubia et
incerta sint hoc tamen ipsum quod Deus non sit fallax quodque idcirco
fieri non possit ut ulla falsitas in meis opinionibus reperiatur nisi
aliqua etiam sit in me facultas a Deo tributa ad illam emendandam
certam mihi spem ostendit veritatis etiam in iis assequendae. Et sane
non dubium est quin ea omnia quae doceor a natura aliquid habeant
veritatis Per naturam enim generaliter spectatam nihil nunc aliud quam
vel Deum ipsum vel rerum creatarum coordinationem a Deo institutam
intelligo. nec aliud per naturam meam in particulari quam complexionem
eorum omnium quae mihi a Deo sunt tributa.

Nihil autem est quod me ista natura magis expresse doceat quam quod
habeam corpus cui male est cum dolorem sentio quod cibo vel potu
indiget cum famem aut sitim patior et similia. nec proinde dubitare
debeo quin aliquid in eo sit veritatis.

Docet etiam natura per istos sensus doloris famis sitis etc. 
me non tantum adesse meo corpori ut nauta adest navigio sed illi
arctissime esse conjunctum et quasi permixtum adeo ut unum quid cum
illo componam. alioqui enim cum corpus laeditur ego qui nihil aliud
sum quam res cogitans non sentirem idcirco dolorem sed puro intellectu
laesionem istam perciperem ut nauta visu percipit si quid in nave
frangatur. et cum corpus cibo vel potu indiget hoc ipsum expresse
intelligerem non confusos famis et sitis sensus haberem. Nam certe isti
sensus sitis famis doloris etc. nihil aliud sunt quam confusi quidam
cogitandi modi ab unione et quasi permixtione mentis cum corpore exorti.

Praeterea etiam doceor a natura varia circa meum corpus alia corpora
existere ex quibus nonnulla mihi prosequenda sunt alia fugienda. Et
certe ex eo quod valde diversos sentiam colores sonos odores sapores
calorem duritiem et similia recte concludo aliquas esse in
corporibus a quibus variae istae sensuum perceptiones 
adveniunt varietates iis respondentes etiamsi forte iis non similes.
atque ex eo quod quaedam ex illis perceptionibus mihi gratae sint
aliae ingratae plane certum est meum corpus sive potius me totum
quatenus ex corpore et mente sum compositus variis commodis et
incommodis a circumjacentibus corporibus affici posse.

Multa vero alia sunt quae etsi videar a natura doctus esse non tamen
revera ab ipsa sed a consuetudine quadam inconsiderate judicandi
accepi atque ideo falsa esse facile contingit. ut quod omne
spatium in quo nihil plane occurrit quod meos sensus moveat sit
vacuum. quod in corpore exempli gratia calido aliquid sit plane simile
ideae caloris quae in me est in albo aut viridi sit eadem albedo aut
viriditas quam sentio. in amaro aut dulci idem sapor et sic de caeteris.
quod et astra et turres et quaevis alia remota corpora ejus sint tantum
magnitudinis et figurae quam sensibus meis exhibent et alia ejusmodi.
Sed ne quid in hac re non satis distincte percipiam accuratius 
debeo definire quid proprie intelligam cum dico me aliquid doceri a
natura. nempe hic naturam strictius sumo quam pro complexione eorum
omnium quae mihi a Deo tributa sunt. in hac enim complexione multa
continentur quae ad mentem solam pertinent ut quod percipiam id quod
factum est infectum esse non posse et reliqua omnia quae lumine
naturali sunt nota de quibus hic non est sermo. multa etiam quae ad
solum corpus spectant ut quod deorsum tendat et similia de quibus
etiam non ago sed de iis tantum quae mihi ut composito ex mente et
corpore a Deo tributa sunt. Ideoque haec natura docet quidem ea refugere
quae sensum doloris inferunt et ea prosequi quae sensum voluptatis et
talia. sed non apparet illam praeterea nos docere ut quicquam ex istis
sensuum perceptionibus sine praevio intellectus examine de rebus extra
nos positis concludamus quia de iis verum scire ad mentem solam non
autem ad compositum videtur pertinere. Ita quamvis stella non magis
oculum meum quam ignis exiguae facis afficiat nulla tamen in eo 
realis sive positiva propensio est ad credendum illam non esse majorem
sed hoc sine ratione ab ineunte aetate judicavi. et quamvis ad ignem
accedens sentio calorem ut etiam ad eundem nimis prope accedens sentio
dolorem nulla profecto ratio est quae suadeat in igne aliquid esse
simile isti calori. ut neque etiam isti dolori sed tantummodo in eo
aliquid esse quodcunque demum sit quod istos in nobis sensus caloris
vel doloris efficiat et quamvis etiam in aliquo spatio nihil sit quod
moveat sensum non ideo sequitur in eo nullum esse corpus sed video me
in his aliisque permultis ordinem naturae pervertere esse assuetum quia
nempe sensuum perceptionibus quae proprie tantum a natura datae sunt ad
menti significandum quaenam composito cujus pars est commoda sint vel
incommoda et eatenus sunt satis clarae et distinctae utor tanquam
regulis certis ad immediate dignoscendum quaenam sit corporum extra nos
positorum essentia de qua tamen nihil nisi valde obscure et 
confuse significant.

Atqui jam ante satis perspexi qua ratione non obstante Dei bonitate
judicia mea falsa esse contingat. Sed nova hic occurrit difficultas
circa illa ipsa quae tanquam persequenda vel fugienda mihi a natura
exhibentur. atque etiam circa internos sensus in quibus errores videor
deprehendisse Ut cum quis grato cibi alicujus sapore delusus venenum
intus latens assumit. Sed nempe tunc tantum a natura impellitur ad illud
appetendum in quo gratus sapor consistit. non autem ad venenum quod
plane ignorat. nihilque hinc aliud concludi potest quam naturam istam
non esse omnisciam quod non mirum quia cum homo sit res limitata non
alia illi competit quam limitatae perfectionis.

At vero non raro etiam in iis erramus ad quae a natura impellimur. ut
cum ii qui aegrotant potum vel cibum appetunt sibi paulo post
nociturum. Dici forsan hic poterit illos ob id errare quod natura eorum
sit corrupta sed hoc difficultatem non tollit quia non minus 
vere homo aegrotus creatura Dei est quam sanus. nec proinde minus
videtur repugnare illum a Deo fallacem naturam habere. Atque ut
horologium ex rotis et ponderibus confectum non minus accurate leges
omnes naturae observat cum male fabricatum est et horas non recte
indicat quam cum omni ex parte artificis voto satisfacit ita si
considerem hominis corpus quatenus machinamentum quoddam est ex ossibus
nervis musculis venis sanguine et pellibus ita aptum et compositum
ut etiamsi nulla in eo mens existeret eosdem tamen haberet omnes motus
qui nunc in eo non ab imperio voluntatis nec proinde a mente
procedunt facile agnosco illi aeque naturale fore si exempli
causa hydrope laboret eam faucium ariditatem pati quae sitis sensum
menti inferre solet atque etiam ab illa ejus nervos et reliquas partes
ita disponi ut potum sumat ex quo morbus augeatur. quam cum nullum tale
in eo vitium est a simili faucium siccitate moveri ad potum 
sibi utilem assumendum. Et quamvis respiciens ad praeconceptum horologii
usum dicere possim illud cum horas non recte indicat a natura sua
deflectere. atque eodem modo considerans machinamentum humani corporis
tanquam comparatum ad motus qui in eo fieri solent putem illud etiam a
natura sua aberrare si ejus fauces sint aridae cum potus ad ipsius
conservationem non prodest. satis tamen animadverto hanc ultimam naturae
acceptionem ab altera multum differre. haec enim nihil aliud est quam
denominatio a cogitatione mea hominem aegrotum et horologium male
fabricatum cum idea hominis sani et horologii recte facti comparante
dependens rebusque de quibus dicitur extrinseca. per illam vero aliquid
intelligo quod revera in rebus reperitur ac proinde nonnihil habet
veritatis.

At certe etiamsi respiciendo ad corpus hydrope laborans sit tantum
denominatio extrinseca cum dicitur ejus natura esse corrupta 
ex eo quod aridas habeat fauces nec tamen egeat potu. respiciendo tamen
ad compositum sive ad mentem tali corpori unitam non est pura
denominatio sed verus error naturae quod sitiat cum potus est ipsi
nociturus. ideoque hic remanet inquirendum quo pacto bonitas Dei non
impediat quo minus natura sic sumpta sit fallax.

Nempe imprimis hic adverto magnam esse differentiam inter mentem et
corpus in eo quod corpus ex natura sua sit semper divisibile mens
autem plane indivisibilis. nam sane cum hanc considero sive meipsum
quatenus sum tantum res cogitans nullas in me partes possum
distinguere sed rem plane unam et integram me esse intelligo et
quamvis toti corpori tota mens unita esse videatur abscisso tamen pede
vel brachio vel quavis alia corporis parte nihil ideo de mente
subductum esse cognosco. neque etiam facultates volendi sentiendi
intelligendi etc. ejus partes dici possunt quia una et eadem mens est
quae vult quae sentit quae intelligit. Contra vero nulla res 
corporea sive extensa potest a me cogitari quam non facile in
partes cogitatione dividam atque hoc ipso illam divisibilem esse
intelligam quod unum sufficeret ad me docendum mentem a corpore omnino
esse diversam si nondum illud aliunde satis scirem.

Deinde adverto mentem non ab omnibus corporis partibus immediate affici
sed tantummodo a cerebro vel forte etiam ab una tantum exigua ejus
parte nempe ab ea in qua dicitur esse sensus communis. quae
quotiescunque eodem modo est disposita menti idem exhibet etiamsi
reliquae corporis partes diversis interim modis possint se habere ut
probant innumera experimenta quae hic recensere non est opus.

Adverto praeterea eam esse corporis naturam ut nulla ejus pars possit
ab alia parte aliquantum remota moveri quin possit etiam moveri eodem
modo a qualibet ex iis quae interjacent quamvis illa remotior nihil
agat. Ut exempli causa in fune A B C D si trahatur ejus ultima
pars D non alio pacto movebitur prima A quam moveri etiam 
posset si traheretur una ex intermediis B vel C et ultima D maneret
immota. Nec dissimili ratione cum sentio dolorem pedis docuit me
Physica sensum illum fieri ope nervorum per pedem sparsorum qui inde
ad cerebrum usque funium instar extensi dum trahuntur in pede trahunt
etiam intimas cerebri partes ad quas pertingunt quemdamque motum in iis
excitant qui institutus est a natura ut mentem afficiat sensu doloris
tanquam in pede existentis. Sed quia illi nervi per tibiam crus
lumbos dorsum et collum transire debent ut a pede ad cerebrum
perveniant potest contingere ut etiamsi eorum pars quae est in pede
non attingatur sed aliqua tantum ex intermediis idem plane ille motus
fiat in cerebro qui fit pede male affecto ex quo necesse erit ut mens
sentiat eundem dolorem et idem de quolibet alio sensu est putandum.

Adverto denique quandoquidem unusquisque ex motibus qui fiunt in ea
 parte cerebri quae immediate mentem afficit non nisi unum 
aliquem sensum illi infert nihil hac in re melius posse excogitari
quam si eum inferat qui ex omnibus quos inferre potest ad hominis sani
conservationem quam maxime et quam frequentissime conducit.
Experientiam autem testari tales esse omnes sensus nobis a natura
inditos. ac proinde nihil plane in iis reperiri quod non Dei
potentiam bonitatemque testetur. Ita exempli causa cum nervi qui sunt
in pede vehementer et praeter consuetudinem moventur ille eorum motus
per spinae dorsi medullam ad intima cerebri pertingens ibi menti signum
dat ad aliquid sentiendum nempe dolorem tanquam in pede existentem a
quo illa excitatur ad ejus causam ut pedi infestam quantum in se est
amovendam. Potuisset vero natura hominis a Deo sic constitui ut ille
idem motus in cerebro quidvis aliud menti exhiberet. nempe vel seipsum
quatenus est in cerebro. vel quatenus est in pede. vel in aliquo ex
locis intermediis vel denique aliud quidlibet. sed nihil aliud 
ad corporis conservationem aeque conduxisset. Eodem modo cum potu
indigemus quaedam inde oritur siccitas in gutture nervos ejus movens
et illorum ope cerebri interiora. hicque motus mentem afficit sensu
sitis quia nihil in toto hoc negotio nobis utilius est scire quam quod
potu ad conservationem valetudinis egeamus et sic de caeteris.

Ex quibus omnino manifestum est non obstante immensa Dei bonitate
naturam hominis ut ex mente et corpore compositi non posse non aliquando
esse fallacem. Nam si quae causa non in pede sed in alia quavis ex
partibus per quas nervi a pede ad cerebrum porriguntur vel etiam in
ipso cerebro eundem plane motum excitet qui solet excitari pede male
affecto sentietur dolor tanquam in pede sensusque naturaliter
falletur quia cum ille idem motus in cerebro non possit nisi eundem
semper sensum menti inferre multoque frequentius oriri soleat a causa
quae laedit pedem quam ab alia alibi existente rationi consentaneum
est ut pedis potius quam alterius partis dolorem menti semper 
exhibeat. Et si quando faucium ariditas non ut solet ex eo quod ad
corporis valetudinem potus conducat sed ex contraria aliqua causa
oriatur ut in hydropico contingit longe melius est illam tunc fallere
quam si contra semper falleret cum corpus est bene constitutum et sic
de reliquis.

Atque haec consideratio plurimum juvat non modo ut errores omnes quibus
natura mea obnoxia est animadvertam sed etiam ut illos aut emendare
aut vitare facile possim. Nam sane cum sciam omnes sensus circa ea quae
ad corporis commodum spectant multo frequentius verum indicare quam
falsum possimque uti fere semper pluribus ex iis ad eandem rem
examinandam. et insuper memoria. quae praesentia cum praecedentibus
connectit. et intellectu qui jam omnes errandi causas perspexit non
amplius vereri debeo ne illa quae mihi quotidie a sensibus exhibentur
sint falsa sed hyperbolicae superiorum dierum dubitationes ut risu
dignae sunt explodendae. Praesertim summa illa de somno quem a vigilia
non distinguebam. nunc enim adverto permagnum inter utrumque esse
discrimen in eo quod nunquam insomnia cum reliquis omnibus actionibus
vitae a memoria conjungantur ut ea quae vigilanti occurrunt. nam sane
si quis dum vigilo mihi derepente appareret statimque postea
dispareret ut fit in somnis ita scilicet ut nec unde venisset nec
quo abiret viderem non immerito spectrum potius aut phantasma in
cerebro meo effictum quam verum hominem esse judicarem. Cum vero eae
res occurrunt quas distincte unde ubi et quando mihi adveniant
adverto earumque perceptionem absque ulla interruptione cum tota
reliqua vita connecto plane certus sum non in somnis sed vigilanti
occurrere. Nec de ipsarum veritate debeo vel minimum dubitare si
postquam omnes sensus memoriam et intellectum ad illas examinandas
convocavi nihil mihi quod cum caeteris pugnet ab ullo ex his nuntietur.
Ex eo enim quod Deus non sit fallax sequitur omnino in talibus me non
falli. Sed quia rerum agendarum necessitas non semper tam
accurati examinis moram concedit fatendum est humanam vitam circa res
particulares saepe erroribus esse obnoxiam et naturae nostrae
infirmitas est agnoscenda.
";

  private static readonly Regex LowerAndWhitespaceOnly = new(pattern: "(([a-z])|(\\ ))*", RegexOptions.Compiled);

  public static IEnumerable<string> ParseIntoWords(LoremIpsumSource source)
  {
    return source switch
    {
      LoremIpsumSource.CiceroDeFinibusBonorumEtMalorum => LowerAndWhitespaceOnly
        .Matches(input: CiceroDeFinibusBonorumEtMalorum.ToLowerInvariant())
        .SelectMany(item => item.Value.Trim().Split(' '))
        .Where(word => !string.IsNullOrWhiteSpace(word)),
      LoremIpsumSource.MeditatioIvDeVeroEtFalso => LowerAndWhitespaceOnly
        .Matches(input: MeditatioIvDeVeroEtFalso.ToLowerInvariant())
        .SelectMany(item => item.Value.Trim().Split(' '))
        .Where(word => !string.IsNullOrWhiteSpace(word)),
      LoremIpsumSource.MeditatioViDeRerumMaterialiumExistentiaEtRealiMentisACorporeDistinctione =>
        LowerAndWhitespaceOnly
          .Matches(input: MeditatioViDeRerumMaterialiumExistentiaEtRealiMentisACorporeDistinctione.ToLowerInvariant())
          .SelectMany(item => item.Value.Trim().Split(' '))
          .Where(word => !string.IsNullOrWhiteSpace(word)),
      _ => throw new ArgumentOutOfRangeException(paramName: nameof(source), source, null)
    };
  }
}
