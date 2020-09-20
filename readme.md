# MonstercatNet

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/Insire/Maple/blob/master/license.md)
![NuGet](https://img.shields.io/nuget/v/MonstercatNet)
[![Build status](https://dev.azure.com/SoftThorn/MonstercatNet/_apis/build/status/MonstercatNet-CD)](https://dev.azure.com/SoftThorn/MonstercatNet/_build/latest?definitionId=3)
[![CodeFactor](https://www.codefactor.io/repository/github/insire/monstercatnet/badge)](https://www.codefactor.io/repository/github/insire/monstercatnet)
[![codecov](https://codecov.io/gh/Insire/MonstercatNet/branch/master/graph/badge.svg)](https://codecov.io/gh/Insire/MonstercatNet)

MonstercatNet is a .NET wrapper around the API that drives [monstercat.com](https://www.monstercat.com/) written in C#.

## Supported Platforms

Since this library relies on [refit](https://github.com/reactiveui/refit) for setting up the API endpoints, their limitations transfer over to this. You can find the limitations [here](https://github.com/reactiveui/refit#where-does-this-work).

## Usage

TODO

## Endpoints

The currently implemented and support endpoints of the monstercat api can be found [here](endpoints.md)

## Download

You can find the MonstercatNet nuget package [here](https://www.nuget.org/packages/MonstercatNet/).

## Versioning

MonstercatNet uses the following versioning strategy:

|number|description|
| - | - |
|major|mirrors the version of the supported monstercat api version|
|minor|major version accroding to [semver](https://semver.org/) (changes when incompatible API changes were made))|
|build|minor version accroding to [semver](https://semver.org/) (changes when functionality or bugfixes in a backwards compatible manner were added|
----
A special thanks goes out to [defvs](https://github.com/defvs/connect-v2-docs) who with many others documented the unofficial API.
