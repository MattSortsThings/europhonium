# System requirements

This document outlines the system requirements for the *Europhonium* project.

- [System requirements](#system-requirements)
  - [User profiles](#user-profiles)
    - [Admin](#admin)
    - [EuroFan](#eurofan)
  - [Summary](#summary)
  - [A - Admin API requirements](#a---admin-api-requirements)
    - [A/1 - Countries](#a1---countries)
      - [A/1/1 - Create Country](#a11---create-country)
      - [A/1/2 - Get Country](#a12---get-country)
      - [A/1/3 - Get Countries](#a13---get-countries)
      - [A/1/4 - Delete Country](#a14---delete-country)
    - [A/2 - Contests](#a2---contests)
      - [A/2/1 - Create Country](#a21---create-country)
      - [A/2/2 - Get Contest](#a22---get-contest)
      - [A/2/3 - Get Countries](#a23---get-countries)
      - [A/2/4 - Delete Contest](#a24---delete-contest)
      - [A/2/5 - Create Broadcast](#a25---create-broadcast)
    - [A/3 - Broadcasts](#a3---broadcasts)
      - [A/3/1 - Get Broadcast](#a31---get-broadcast)
      - [A/3/2 - Get Countries](#a32---get-countries)
      - [A/3/3 - Delete Broadcast](#a33---delete-broadcast)
      - [A/3/4 - Award Jury Points](#a34---award-jury-points)
      - [A/3/5 - Award Televote Points](#a35---award-televote-points)
  - [P - Public API requirements](#p---public-api-requirements)
    - [P/1 - Facets](#p1---facets)
      - [P/1/1 - Get Broadcast Codes](#p11---get-broadcast-codes)
      - [P/1/2 - Get Contest Years](#p12---get-contest-years)
      - [P/1/3 - Get Country Codes](#p13---get-country-codes)
    - [P/2 - Rankings](#p2---rankings)
      - [P/2/1 - Broadcast Competitor Rankings](#p21---broadcast-competitor-rankings)
        - [P/2/1/1 - Get Points Average Broadcast Competitor Rankings](#p211---get-points-average-broadcast-competitor-rankings)
        - [P/2/1/2 - Get Points Consensus Broadcast Competitor Rankings](#p212---get-points-consensus-broadcast-competitor-rankings)
        - [P/2/1/3 - Get Points In Range Broadcast Competitor Rankings](#p213---get-points-in-range-broadcast-competitor-rankings)
        - [P/2/1/4 - Get Points Share Broadcast Competitor Rankings](#p214---get-points-share-broadcast-competitor-rankings)
      - [P/2/2 - Contest Competitor Rankings](#p22---contest-competitor-rankings)
        - [P/2/2/1 - Get Points Average Contest Competitor Rankings](#p221---get-points-average-contest-competitor-rankings)
        - [P/2/2/2 - Get Points Consensus Contest Competitor Rankings](#p222---get-points-consensus-contest-competitor-rankings)
        - [P/2/2/3 - Get Points In Range Contest Competitor Rankings](#p223---get-points-in-range-contest-competitor-rankings)
        - [P/2/2/4 - Get Points Share Contest Competitor Rankings](#p224---get-points-share-contest-competitor-rankings)
      - [P/2/3 - Overall Competitor Rankings](#p23---overall-competitor-rankings)
        - [P/2/3/1 - Get Points Average Overall Competitor Rankings](#p231---get-points-average-overall-competitor-rankings)
        - [P/2/3/2 - Get Points Consensus Overall Competitor Rankings](#p232---get-points-consensus-overall-competitor-rankings)
        - [P/2/3/3 - Get Points In Range Overall Competitor Rankings](#p233---get-points-in-range-overall-competitor-rankings)
        - [P/2/3/4 - Get Points Share Overall Competitor Rankings](#p234---get-points-share-overall-competitor-rankings)
      - [P/2/4 - Overall Voter Rankings](#p24---overall-voter-rankings)
        - [P/2/4/1 - Get Points Average Overall Voter Rankings](#p241---get-points-average-overall-voter-rankings)
        - [P/2/4/2 - Get Points Consensus Overall Voter Rankings](#p242---get-points-consensus-overall-voter-rankings)
        - [P/2/4/3 - Get Points In Range Overall Voter Rankings](#p243---get-points-in-range-overall-voter-rankings)
        - [P/2/4/4 - Get Points Share Overall Voter Rankings](#p244---get-points-share-overall-voter-rankings)
        - [P/2/4/5 - Get Points Similarity Overall Voter Rankings](#p245---get-points-similarity-overall-voter-rankings)
  - [S - Shared requirements](#s---shared-requirements)
    - [S/1 - Documentation](#s1---documentation)
      - [S/1/1 - Swagger documents (completed 27/11/2024)](#s11---swagger-documents-completed-27112024)
    - [S/2 - Error handling](#s2---error-handling)
      - [S/2/1 - Railway architecture (completed 27/11/2024)](#s21---railway-architecture-completed-27112024)
      - [S/2/2 - Global exception handling (completed 27/11/2024)](#s22---global-exception-handling-completed-27112024)
    - [S/3 - Security](#s3---security)
      - [S/3/1 - Admin API key (completed 27/11/2024)](#s31---admin-api-key-completed-27112024)
      - [S/3/2 - Public API key (completed 27/11/2024)](#s32---public-api-key-completed-27112024)

## User profiles

### Admin

The Admin is a single developer (me) responsible for maintaining and updating the data in the system. The Admin is very familiar with the Eurovision domain model and how it is modelled in the system.

### EuroFan

The EuroFan is an anonymous member of the public located anywhere in the world, who is interested in the trends and minutiae of Eurovision voting patterns. The EuroFan is familiar with the rules of Eurovision, but they are not interested in the technical aspects of how the domain is modelled in the system.

## Summary

*Europhonium* is a web API subdivided into two web APIs: the *Admin API* and the *Public API*.

As the Admin, I want to use the *Admin API* to populate the system with data from past editions of the Eurovision Song Contest in a manner that replicates the way the points are collected in each broadcast.

As a EuroFan, I want to use the *Public API* to perform a wide range of ranking queries based on different ways of analysing the voting patterns of the Eurovision Song Contest 2016-present.

## A - Admin API requirements

### A/1 - Countries

#### A/1/1 - Create Country

As the Admin, I want to create a new country in the system, so that I can reference it in contests and broadcasts that I will go on to create.

#### A/1/2 - Get Country

As the Admin, I want to retrieve a single country, identified by supplying its ID, so that I can see the contests and/or broadcasts in which it is involved.

#### A/1/3 - Get Countries

As the Admin, I want to retrieve all the countries in the system, ordered alphabetically by country code, so that I can see what countries I need to create.

#### A/1/4 - Delete Country

As the Admin, I want to delete a single country from the system, identified by supplying its ID, so that I can recreate it with corrected details.

### A/2 - Contests

#### A/2/1 - Create Country

As the Admin, I want to create a new contest in the system so that I can reference it in broadcasts that I will go on to create.

#### A/2/2 - Get Contest

As the Admin, I want to retrieve a single contest, identified by supplying its ID, so that I can see the broadcasts in which it is involved.

#### A/2/3 - Get Countries

As the Admin, I want to retrieve all the contests in the system, ordered numerically by contest year, so that I can see what contests I need to create.

#### A/2/4 - Delete Contest

As the Admin, I want to delete a single contest from the system, identified by supplying its ID, so that I can recreate it with corrected details.

#### A/2/5 - Create Broadcast

As the Admin, I want to create a new broadcast from a contest, identified by supplying its ID, so that I can award the points in the broadcast.

### A/3 - Broadcasts

#### A/3/1 - Get Broadcast

As the Admin, I want to retrieve a single broadcast, identified by supplying its ID, so that I can see which voting countries' points are still to be awarded.

#### A/3/2 - Get Countries

As the Admin, I want to retrieve all the broadcasts in the system, ordered alphabetically by broadcast code, so that I can see what broadcasts I need to create.

#### A/3/3 - Delete Broadcast

As the Admin, I want to delete a single broadcast from the system, identified by supplying its ID, so that I can recreate it with corrected details.

#### A/3/4 - Award Jury Points

As the Admin, I want to award the jury points for a single voting country in a single broadcast, identified by supplying their IDs, so that the voting data can eventually be included in the *Public API*'s queried data.

#### A/3/5 - Award Televote Points

As the Admin, I want to award the televote points for a single voting country in a single broadcast, identified by supplying their IDs, so that the voting data can eventually be included in the *Public API*'s queried data.

## P - Public API requirements

### P/1 - Facets

#### P/1/1 - Get Broadcast Codes

As a EuroFan, I want to retrieve a sorted list of all the available broadcast codes in the system and their corresponding broadcasts, so that I can target my queries by broadcast code.

#### P/1/2 - Get Contest Years

As a EuroFan, I want to retrieve a sorted list of all the available contest years in the system and their corresponding contests, so that I can target my queries by contest year.

#### P/1/3 - Get Country Codes

As a EuroFan, I want to retrieve a sorted list of all the available country codes in the system and their corresponding countries, so that I can target my queries by country code.

### P/2 - Rankings

#### P/2/1 - Broadcast Competitor Rankings

##### P/2/1/1 - Get Points Average Broadcast Competitor Rankings

As a EuroFan, I want to rank each competing country in each broadcast by the **average value of all the points awards it received**, with optional filtering of queried data by broadcast code and/or voting method, and optional sorting and pagination of results.

##### P/2/1/2 - Get Points Consensus Broadcast Competitor Rankings

As a EuroFan, I want to rank each competing country in each broadcast by the **cosine similarity of the jury and televote points awards it received**, with optional filtering of queried data by broadcast code, and optional sorting and pagination of results.

##### P/2/1/3 - Get Points In Range Broadcast Competitor Rankings

As a EuroFan, I want to rank each competing country in each broadcast by the **relative frequency of the points awards it received having values in a specified range**, with optional filtering of queried data by broadcast code and/or voting method, and optional sorting and pagination of results.

##### P/2/1/4 - Get Points Share Broadcast Competitor Rankings

As a EuroFan, I want to rank each competing country in each broadcast by the **total points it received as a fraction of the maximum possible points it could have received**, with optional filtering of queried data by broadcast code and/or voting method, and optional sorting and pagination of results.

#### P/2/2 - Contest Competitor Rankings

##### P/2/2/1 - Get Points Average Contest Competitor Rankings

As a EuroFan, I want to rank each competing country in each contest by the **average value of all the points awards it received**, with optional filtering of queried data by contest year and/or voting method, and optional sorting and pagination of results.

##### P/2/2/2 - Get Points Consensus Contest Competitor Rankings

As a EuroFan, I want to rank each competing country in each contest by the **cosine similarity of the jury and televote points awards it received**, with optional filtering of queried data by contest year, and optional sorting and pagination of results.

##### P/2/2/3 - Get Points In Range Contest Competitor Rankings

As a EuroFan, I want to rank each competing country in each contest by the **relative frequency of the points awards it received having values in a specified range**, with optional filtering of queried data by contest years and/or voting method, and optional sorting and pagination of results.

##### P/2/2/4 - Get Points Share Contest Competitor Rankings

As a EuroFan, I want to rank each competing country in each contest by the **total points it received as a fraction of the maximum possible points it could have received**, with optional filtering of queried data by contest year and/or voting method, and optional sorting and pagination of results.

#### P/2/3 - Overall Competitor Rankings

##### P/2/3/1 - Get Points Average Overall Competitor Rankings

As a EuroFan, I want to rank each competing country overall by the **average value of all the points awards it received**, with optional filtering of queried data by contest year range and/or grand finals only and/or voting country code and/or voting method, and optional sorting and pagination of results.

##### P/2/3/2 - Get Points Consensus Overall Competitor Rankings

As a EuroFan, I want to rank each competing country overall by the **cosine similarity of the jury and televote points awards it received**, with optional filtering of queried data by contest year range and/or grand finals only and/or voting country code, and optional sorting and pagination of results.

##### P/2/3/3 - Get Points In Range Overall Competitor Rankings

As a EuroFan, I want to rank each competing country overall by the **relative frequency of the points awards it received having values in a specified range**, with optional filtering of queried data by contest year range and/or grand finals only and/or voting country code and/or voting method, and optional sorting and pagination of results.

##### P/2/3/4 - Get Points Share Overall Competitor Rankings

As a EuroFan, I want to rank each competing country overall by the **total points it received as a fraction of the maximum possible points it could have received**, with optional filtering of queried data by contest year range and/or grand finals only and/or voting country code and/or voting method, and optional sorting and pagination of results.

#### P/2/4 - Overall Voter Rankings

##### P/2/4/1 - Get Points Average Overall Voter Rankings

As a EuroFan, I want to rank each voting country overall by the **average value of all the points awards it gave to the specified competing country**, with optional filtering of queried data by contest year range and/or grand finals only and/or voting method, and optional sorting and pagination of results.

##### P/2/4/2 - Get Points Consensus Overall Voter Rankings

As a EuroFan, I want to rank each voting country overall by the **cosine similarity of the jury and televote points awards it gave**, with optional filtering of queried data by contest year range and/or grand finals only and/or voting country code and/or competing country code, and optional sorting and pagination of results.

##### P/2/4/3 - Get Points In Range Overall Voter Rankings

As a EuroFan, I want to rank each voting country overall by the **relative frequency of the points awards it gave to the specified competing country having values in a specified range**, with optional filtering of queried data by contest year range and/or grand finals only and/or voting method, and optional sorting and pagination of results.

##### P/2/4/4 - Get Points Share Overall Voter Rankings

As a EuroFan, I want to rank each voting country overall by the **total points it gave to the specified competing country as a fraction of the maximum possible points it could have given**, with optional filtering of queried data by contest year range and/or grand finals only and/or voting method, and optional sorting and pagination of results.

##### P/2/4/5 - Get Points Similarity Overall Voter Rankings

As a EuroFan, I want to rank each voting country overall by the **cosine similarity of the points awards it gave compared with those given by the specified voting country**, with optional filtering of queried data by contest year range and/or grand finals only and/or voting method, and optional sorting and pagination of results.

## S - Shared requirements

### S/1 - Documentation

#### S/1/1 - Swagger documents (completed 27/11/2024)

The system will serve two Swagger documents in development and production: one for the *Admin API* and one for the *Public API*.

### S/2 - Error handling

#### S/2/1 - Railway architecture (completed 27/11/2024)

Every endpoint class uses the same railway architecture. The endpoint maps an incoming request to an application query/command object and dispatches it to the app pipeline. The pipeline returns *either* an instance of the designated app result type for the query/command type *or* an `Error`. The endpoint maps an app result object to a successful `IResult` object, and maps an `Error` to a `ProblemHttpResult` object containing a `ProblemDetails` object.

#### S/2/2 - Global exception handling (completed 27/11/2024)

If an uncaught exception is thrown when handling a request, the response has status code `500` and contains a serialized problem details object with the name of the exception but no implementation-specific details.

### S/3 - Security

#### S/3/1 - Admin API key (completed 27/11/2024)

A request to an *Admin API* endpoint must include the secret *Admin API key* as an `"X-Api-Key"` request header value.

#### S/3/2 - Public API key (completed 27/11/2024)
A request to a *Public API* endpoint must include either the *Public API key* or the secret *Admin API key* as an `"X-Api-Key"` request header value.
