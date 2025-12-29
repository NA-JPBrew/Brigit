# Brigit Framework

AviUtl / AviUtl2 / YMM4 のプラグインを単一のプロジェクトで開発するためのフレームワークです。

## 機能紹介

- **全プラットフォーム**: C# (NET 10) で書かれた1つのプロジェクトから、YMM4とAviUtl両方のプラグインを生成します。
- **バージョンチェッカー**: 生成されたプラグインにはGitHub Releasesを確認する自動アップデートチェック機能 (`VersionChecker.cs`) が標準搭載されています。
- **ライセンス生成**: プロジェクト作成時にMITライセンスファイルを自動生成します。
- **NativeAOTサポート**: NativeAOTを使用してAviUtl用のネイティブDLL (.auf) をC#から直接ビルドします。
- **テンプレート**: 映像・音声エフェクト、図形、ツールウィンドウなどの主要なプラグイン機能のサンプルコードを一度に生成します。

## インストール
以下のコマンドをPowerShellで実行すると、最新版のBrigitがインストールされます。

```powershell
iwr -useb https://raw.githubusercontent.com/TeamBrigit/Brigit/master/install.ps1 | iex
```

## 使い方

### プロジェクトの作成
Brigit CLI を使用して新しいプロジェクトを作成します。

```bash
brigit create
```

実行すると以下の入力を求められます:
1. プロジェクト名の入力
2. エディタの選択 (VS / VS Code)
3. **YMM4インストールパスの指定**: ビルドに必要な参照を解決します。
4. **プロジェクトの自動生成**: 開発環境が一瞬で整います!!

### サンプルについて

上記の `template` コマンドを実行すると、以下のサンプルを含むプロジェクトが生成できます。
開発の参考にしてください。

- `Effects/`: 映像・音声エフェクトのサンプル
- `Items/`: 図形・カスタムオブジェクトのサンプル (IShapePlugin)
- `Transitions/`: 場面切り替えエフェクトのサンプル
- `Windows/`: ツールウィンドウのサンプル

※ リポジトリに含まれるサンプルプロジェクトフォルダは `.gitignore` 対象となっているため、実際に `create` コマンドを試して生成されたコードをご確認ください。

## ビルド方法

出力: `bin/Release/net10.0/BrigitSampleOneCode.dll`

### 2. AviUtlプラグインとしてのビルド (NativeAOT)
NativeAOTを使用して、WindowsネイティブのDLLを生成します。
```bash
dotnet publish -c Release -r win-x64 /p:NativeLib=Shared /p:SelfContained=true
```
出力: `bin/Release/net10.0/win-x64/publish/BrigitSampleOneCode.dll`
→ このファイルを `BrigitSampleOneCode.auf` (または .aui/.auo) にリネームしてAviUtlに導入します。

## 配布用パッケージの作成
`.ymme` ファイルを作成するには `pack` コマンドを使用します。

```bash
Brigit.CLI.exe pack --path ./BrigitSample
# ./BrigitSample/dist/BrigitSample.ymme が生成されます
```
