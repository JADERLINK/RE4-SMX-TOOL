# RE4-SMX-TOOL
Extract and repack RE4 SMX files (RE4 2007/PS2/UHD/PS4/NS/GC/WII/X360/PS3)

**Translate from Portuguese Brazil**

Programa destinado a extrair e recompactar os arquivos SMX do RE4;

**Update V.1.1**
<br>Atualizado a tool para ser compatível com os arquivo SMX big endian, sendo compatível com o arquivo idxsmx da versão anterior;
<br>Adicionado explicação com os nomes "reais" dos campos, e o conteúdo dos enums/flags;

## Extract:

Extrai o arquivo .SMX com o .exe da versão do seu jogo.
<br> Ao extrair o arquivo, será criado o arquivo .idxsmx

## Repack:

Ao fazer o repack escolha o .exe da versão do seu jogo de destino.
<br> Para fazer o repack é necessário o arquivo .idxsmx

## Versions:

RE4_SMX_TOOL_BIG.exe : GC/WII/XBOX360/PS3
<br>RE4_SMX_TOOL_UHD.exe : UHD/PS4/NS
<br>RE4_SMX_TOOL_PS2.exe : 2007/PS2

## Arquivo .idxsmx:

Para saber a estrutura do arquivo .idxsmx leia o conteúdo do arquivo "FieldExplanation.txt", presente nesse repositório.
<br> Veja também o arquivo "FieldExplanationNewFields.txt" e "SMX_Zatarita.c"
<br> Cada "Mode" tem nome de campos diferentes para serem preenchidos,
<br> Caso você mude o "Mode" de uma Smx entry, use o arquivo "FieldReference.idxsmx.txt", para saber o nome dos campos para esse "Mode";

**At.te: JADERLINK**
<br>2025-07-07