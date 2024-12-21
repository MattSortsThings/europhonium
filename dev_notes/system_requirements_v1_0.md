# System requirements: v1.0

This document outlines the version 1.0 system requirements for the *Europhonium* project.

- [System requirements: v1.0](#system-requirements-v10)
  - [Summary](#summary)
  - [Technical requirements](#technical-requirements)
  - [User profiles](#user-profiles)
    - [The Euro-Fan and the *Public API*](#the-euro-fan-and-the-public-api)
    - [The Admin and the *Admin API*](#the-admin-and-the-admin-api)
  - [A1.0 - *Admin API* requirements](#a10---admin-api-requirements)
    - [A1.0a - Countries](#a10a---countries)
      - [A1.0a1 - Create Country](#a10a1---create-country)
      - [A1.0a2 - Get Country](#a10a2---get-country)
      - [A1.0a3 - Get Countries](#a10a3---get-countries)
      - [A1.0a4 - Delete Country](#a10a4---delete-country)
    - [A1.0b - Contests](#a10b---contests)
      - [A1.0b1 - Create Contest](#a10b1---create-contest)
      - [A1.0b2 - Create Broadcast From Contest](#a10b2---create-broadcast-from-contest)
      - [A1.0b3 - Get Contest](#a10b3---get-contest)
      - [A1.0b4 - Get Contests](#a10b4---get-contests)
      - [A1.0b5 - Delete Contest](#a10b5---delete-contest)
    - [A1.0c - Broadcasts](#a10c---broadcasts)
      - [A1.0c1 - Add Televote Awards](#a10c1---add-televote-awards)
      - [A1.0c2 - Add Jury Awards](#a10c2---add-jury-awards)
      - [A1.0c3 - Get Broadcast](#a10c3---get-broadcast)
      - [A1.0c4 - Get Broadcast Summaries](#a10c4---get-broadcast-summaries)
      - [A1.0c5 - Delete Broadcast](#a10c5---delete-broadcast)
    - [A1.0d - Non-Functional](#a10d---non-functional)
      - [A1.0d1 - *Admin API* OpenAPI Document](#a10d1---admin-api-openapi-document)
      - [A1.0d2 - *Admin API* Security](#a10d2---admin-api-security)
  - [P1.0 - *Public API* requirements](#p10---public-api-requirements)
    - [P1.0a - Queryable Contests](#p10a---queryable-contests)
      - [P1.0a1 - Get Queryable Contests](#p10a1---get-queryable-contests)
    - [P1.0b - Queryable Broadcasts](#p10b---queryable-broadcasts)
      - [P1.0b1 - Get Queryable Broadcasts](#p10b1---get-queryable-broadcasts)
    - [P1.0c - Queryable Countries](#p10c---queryable-countries)
      - [P1.0c1 - Get Queryable Countries](#p10c1---get-queryable-countries)
    - [P1.0d - Broadcast Competitor Rankings](#p10d---broadcast-competitor-rankings)
      - [P1.0d1 - Get Points Average Broadcast Competitor Rankings](#p10d1---get-points-average-broadcast-competitor-rankings)
      - [P1.0d2 - Get Points Consensus Broadcast Competitor Rankings](#p10d2---get-points-consensus-broadcast-competitor-rankings)
      - [P1.0d3 - Get Points In Range Broadcast Competitor Rankings](#p10d3---get-points-in-range-broadcast-competitor-rankings)
      - [P1.0d4 - Get Points Share Broadcast Competitor Rankings](#p10d4---get-points-share-broadcast-competitor-rankings)
    - [P1.0e - Contest Competitor Rankings](#p10e---contest-competitor-rankings)
      - [P1.0e1 - Get Points Average Contest Competitor Rankings](#p10e1---get-points-average-contest-competitor-rankings)
      - [P1.0e2 - Get Points Consensus Contest Competitor Rankings](#p10e2---get-points-consensus-contest-competitor-rankings)
      - [P1.0e3 - Get Points In Range Contest Competitor Rankings](#p10e3---get-points-in-range-contest-competitor-rankings)
      - [P1.0e4 - Get Points Share Contest Competitor Rankings](#p10e4---get-points-share-contest-competitor-rankings)
    - [P1.0f - Overall Competitor Rankings](#p10f---overall-competitor-rankings)
      - [P1.0f1 - Get Points Average Overall Competitor Rankings](#p10f1---get-points-average-overall-competitor-rankings)
      - [P1.0f2 - Get Points Consensus Overall Competitor Rankings](#p10f2---get-points-consensus-overall-competitor-rankings)
      - [P1.0f3 - Get Points In Range Overall Competitor Rankings](#p10f3---get-points-in-range-overall-competitor-rankings)
      - [P1.0f4 - Get Points Share Overall Competitor Rankings](#p10f4---get-points-share-overall-competitor-rankings)
    - [P1.0g - Overall Voter Rankings](#p10g---overall-voter-rankings)
      - [P1.0g1 - Get Points Average Overall Voter Rankings](#p10g1---get-points-average-overall-voter-rankings)
      - [P1.0g2 - Get Points Consensus Overall Voter Rankings](#p10g2---get-points-consensus-overall-voter-rankings)
      - [P1.0g3 - Get Points In Range Overall Voter Rankings](#p10g3---get-points-in-range-overall-voter-rankings)
      - [P1.0g4 - Get Points Share Overall Voter Rankings](#p10g4---get-points-share-overall-voter-rankings)
    - [P1.0h - Non-Functional](#p10h---non-functional)
      - [P1.0h1 - *Public API* OpenAPI Document](#p10h1---public-api-openapi-document)
      - [P1.0h2 - *Public API* Security](#p10h2---public-api-security)
  - [S1.0 - Shared requirements](#s10---shared-requirements)
    - [S1.0a - Non-Functional](#s10a---non-functional)
      - [S1.0a1 - Problem Details](#s10a1---problem-details)
      - [S1.0a2 - Global Exception Handling](#s10a2---global-exception-handling)
      - [S1.0a3 - Versioning](#s10a3---versioning)
      - [S1.0a4 - OpenAPI Pages](#s10a4---openapi-pages)
      - [S1.0a5 - Rate Limiting](#s10a5---rate-limiting)
  - [Features out of scope](#features-out-of-scope)

## Summary

- *Europhonium* is a .NET minimal web API for analysing voting patterns in the Eurovision Song Contest, 2016-present.
- *Europhonium* is composed of two separate APIs with a shared domain and database: the *Public API* and the *Admin API*.
- The *Public API* provides a wide range of configurable analytics queries that can be run on the queryable data held in the system database.
- The *Admin API* provides the means to populate the system database with the queryable data.

## Technical requirements

1. The system uses the **.NET 9** SDK.
2. The system is hosted in the cloud as an Azure Web App.
3. The system database is hosted in the cloud as an Azure SQL Database.
4. The language of the system is UK English.
5. Countries and host cities are referred to using their English-language names following the [official Eurovision website](https://www.eurovision.tv/).

## User profiles

Two user profiles are defined for the purpose of writing requirements: the **Euro-Fan**, who uses the *Public API*, and the **Admin**, who uses the *Admin API*.

### The Euro-Fan and the *Public API*

The Euro-Fan is an anonymous member of the public located anywhere in the world, who interacts with the system using its *Public API*.

The Euro-Fan has at least some familiarity with the Eurovision Song Contest and how it works.

The Euro-Fan uses the *Public API* because they are interested in discovering insights from the way the votes have been distributed in recent contests.

Questions posed by the Euro-Fan may include:

- "Of all the competing countries in all the broadcasts, which competitors received the highest average points award value?"
- "Of all the competing countries in the 2022 contest, which competitor received the highest share of the maximum possible points they could have received?"
- "Of all the competing countries overall, which competitor has received the highest relative frequency of non-zero televote points?"
- "Of all the competing countries overall, in the grand finals of the contests between 2023 and the present, which competitor has had the highest consensus between the jury points and the televote points they received?"
- "Of all the voting countries overall, which voter has awarded jury points most similar to those awarded by the UK?"

### The Admin and the *Admin API*

The Admin is a single software developer (myself), who interacts with the system using its *Admin API*.

As the Admin, I am very familiar with the Eurovision Song Contest and how it works. I am also intimately familiar with the internal workings of the system since I am the sole developer.

As the Admin, I use the *Admin API* because I want to populate the system database with the queryable data to support the *Public API*, that is, the countries, contests and broadcasts from the Eurovision Song Contest, 2016-present.

Additionally, I wished to gain some experience in domain-driven design. This is why the *Admin API* is implemented using aggregates, transactions, domain events, etc., when the system database could just as well be populated using a single SQL script.

## A1.0 - *Admin API* requirements

### A1.0a - Countries

#### A1.0a1 - Create Country

As the Admin, I want to create a new country in the system, so that I can reference it in contests and broadcasts I will go on to create.

#### A1.0a2 - Get Country

As the Admin, I want to retrieve a single country from the system, identified by specifying its ID, so that I can see the contests and/or broadcasts in which it has membership.

#### A1.0a3 - Get Countries

As the Admin, I want to retrieve a list of all the countries in the system, ordered alphabetically by country code, so that I can see which countries I need to collect.

#### A1.0a4 - Delete Country

As the Admin, I want to delete a single country from the system, identified by specifying its ID, so that I can re-create it with different details.

### A1.0b - Contests

#### A1.0b1 - Create Contest

As the Admin, I want to create a new contest in the system, so that I can use it to create its constituent broadcasts.

#### A1.0b2 - Create Broadcast From Contest

As the Admin, I want to create a new broadcast from a single contest in the system, identified by specifying its ID, so that I can start awarding points in the broadcast.

#### A1.0b3 - Get Contest

As the Admin, I want to retrieve a single contest from the system, identified by specifying its ID, so that I can see its details.

#### A1.0b4 - Get Contests

As the Admin, I want to retrieve a list of all the contests in the system, ordered chronologically by contest year, so that I can see which contests I need to collect.

#### A1.0b5 - Delete Contest

As the Admin, I want to delete a single contest from the system, identified by specifying its ID, so that I can re-create it with different details.

### A1.0c - Broadcasts

#### A1.0c1 - Add Televote Awards

As the Admin, I want to add a set of televote awards for a single voter in a single broadcast in the system, identified by specifying their IDs, so that the points can contribute towards the system's queryable data.

#### A1.0c2 - Add Jury Awards

As the Admin, I want to add a set of jury awards for a single voter in a single broadcast in the system, identified by specifying their IDs, so that the points can contribute towards the system's queryable data.

#### A1.0c3 - Get Broadcast

As the Admin, I want to retrieve a single broadcast from the system, identified by specifying its ID, so that I can see its details.

#### A1.0c4 - Get Broadcast Summaries

As the Admin, I want to retrieve a list of all the broadcasts in the system, summarized and ordered chronologically by transmission date, so that I can obtain an overview of their details.

#### A1.0c5 - Delete Broadcast

As the Admin, I want to delete a single broadcast from the system, identified by specifying its ID, so that I can re-create it with different details.

### A1.0d - Non-Functional

#### A1.0d1 - *Admin API* OpenAPI Document

Each minor version of the *Admin API* serves its own OpenAPI document.

#### A1.0d2 - *Admin API* Security

Any HTTP request to an *Admin API* endpoint must include the secret Admin Api Key as an `"X-Api-Key"` request header value. If the Public API Key is used, the request is authenticated but not authorized.

## P1.0 - *Public API* requirements

### P1.0a - Queryable Contests

#### P1.0a1 - Get Queryable Contests

**User story**

As a Euro-Fan, I want to request a list of all the queryable contests in the system, ordered chronologically by contest year, so that I can plan my queries.

### P1.0b - Queryable Broadcasts

#### P1.0b1 - Get Queryable Broadcasts

**User story**

As a Euro-Fan, I want to request a list of all the queryable broadcasts in the system, ordered chronologically by transmission date, so that I can plan my queries.

### P1.0c - Queryable Countries

#### P1.0c1 - Get Queryable Countries

**User story**

As a Euro-Fan, I want to request a list of all the queryable countries in the system, ordered alphabetically by country code, so that I can plan my queries.

### P1.0d - Broadcast Competitor Rankings

#### P1.0d1 - Get Points Average Broadcast Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country in each broadcast, ranked by descending points average, that is, the average value of all the individual points awards it received.

I want to be able to filter the queryable data to points awarded in a specified contest year *and/or* in a specified contest stage *and/or* using a specified voting method.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0d2 - Get Points Consensus Broadcast Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country in each broadcast, ranked by descending points consensus, that is, cosine similarity of all the individual televote and jury points awards it received, using each voting country in the broadcast as a vector dimension.

I want to be able to filter the queryable data to points awarded in a specified contest year *and/or* in a specified contest stage.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0d3 - Get Points In Range Broadcast Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country in each broadcast, ranked by descending points in range, that is, the relative frequency of all the individual points awards it received having a value in a specified range.

I want to be able to filter the queryable data to points awarded in a specified contest year *and/or* in a specified contest stage *and/or* using a specified voting method.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0d4 - Get Points Share Broadcast Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country in each broadcast, ranked by descending points share, that is, the sum total points it received as a fraction of the possible points it could have received.

I want to be able to filter the queryable data to points awarded in a specified contest year *and/or* in a specified contest stage *and/or* using a specified voting method.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

### P1.0e - Contest Competitor Rankings

#### P1.0e1 - Get Points Average Contest Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country in each contest, ranked by descending points average, that is, the average value of all the individual points awards it received.

I want to be able to filter the queryable data to points awarded in a specified contest year *and/or* using a specified voting method.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0e2 - Get Points Consensus Contest Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country in each contest, ranked by descending points consensus, that is, cosine similarity of all the individual televote and jury points awards it received, using each voting country in each broadcast as a vector dimension.

I want to be able to filter the queryable data to points awarded in a specified contest year.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0e3 - Get Points In Range Contest Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country in each contest, ranked by descending points in range, that is, the relative frequency of all the individual points awards it received having a value in a specified range.

I want to be able to filter the queryable data to points awarded in a specified contest year *and/or* using a specified voting method.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0e4 - Get Points Share Contest Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country in each contest, ranked by descending points share, that is, the sum total points it received as a fraction of the possible points it could have received.

I want to be able to filter the queryable data to points awarded in a specified contest year *and/or* using a specified voting method.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

### P1.0f - Overall Competitor Rankings

#### P1.0f1 - Get Points Average Overall Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country overall, ranked by descending points average, that is, the average value of all the individual points awards it received.

I want to be able to filter the queryable data to points awarded in a specified contest year range *and/or* in grand finals only *and/or* using a specified voting method *and/or* by a specified voting country.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0f2 - Get Points Consensus Overall Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country overall, ranked by descending points consensus, that is, cosine similarity of all the individual televote and jury points awards it received, using each voting country in each broadcast as a vector dimension.

I want to be able to filter the queryable data to points awarded in a specified contest year range *and/or* in grand finals only *and/or* by a specified voting country.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0f3 - Get Points In Range Overall Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country overall, ranked by descending points in range, that is, the relative frequency of all the individual points awards it received having a value in a specified range.

I want to be able to filter the queryable data to points awarded in a specified contest year range *and/or* in grand finals only *and/or* using a specified voting method *and/or* by a specified voting country.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0f4 - Get Points Share Overall Competitor Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each competing country overall, ranked by descending points share, that is, the sum total points it received as a fraction of the possible points it could have received.

I want to be able to filter the queryable data to points awarded in a specified contest year range *and/or* in grand finals only *and/or* using a specified voting method *and/or* by a specified voting country.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

### P1.0g - Overall Voter Rankings

#### P1.0g1 - Get Points Average Overall Voter Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each voting country overall, ranked by descending points average, that is, the average value of all the individual points awards it gave to a specified competing country.

I want to be able to filter the queryable data to points awarded in a specified contest year range *and/or* in grand finals only *and/or* using a specified voting method.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0g2 - Get Points Consensus Overall Voter Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each voting country overall, ranked by descending points consensus, that is, cosine similarity of all the individual televote and jury points awards it gave, using each competing country in each broadcast as a vector dimension.

I want to be able to filter the queryable data to points awarded in a specified contest year range *and/or* in grand finals only *and/or* to a specified competing country.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0g3 - Get Points In Range Overall Voter Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each voting country overall, ranked by descending points in range, that is, the relative frequency of all the individual points awards it gave to a specified competing country having a value in a specified range.

I want to be able to filter the queryable data to points awarded in a specified contest year range *and/or* in grand finals only *and/or* using a specified voting method.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

#### P1.0g4 - Get Points Share Overall Voter Rankings

**User story**

As a Euro-Fan, I want to request a page of rankings derived from each voting country overall, ranked by descending points share, that is, the sum total points it gave to a specified competing country as a fraction of the possible points it could have given.

I want to be able to filter the queryable data to points awarded in a specified contest year range *and/or* in grand finals only *and/or* using a specified voting method.

I want to be able to specify the returned rankings sort order *and/or* page index *and/or* items per page.

### P1.0h - Non-Functional

#### P1.0h1 - *Public API* OpenAPI Document

Each minor version of the *Public API* serves its own OpenAPI document.

#### P1.0h2 - *Public API* Security

Any HTTP request to a *Public API* endpoint must include *either* the Public Api Key *or* the secret Admin Api Key as an `"X-Api-Key"` request header value.

## S1.0 - Shared requirements

### S1.0a - Non-Functional

#### S1.0a1 - Problem Details

Any request that results in an `Error` should be mapped to a `ProblemDetails` object and returned as an HTTP result with the appropriate status code.

#### S1.0a2 - Global Exception Handling

If the system throws an uncaught exception while handling an HTTP request, it should return an HTTP response with the `"InternalServerError"` status code and a serialized `ProblemDetails` object stating the name of the exception type but exposing no execution details.

#### S1.0a3 - Versioning

Each API should versioned as a whole, using major/minor semantic versioning as a URL segment, for example:

```http
GET {{host}}adminapi/v1.0countries
```

```http
GET {{host}}/publicapi/v1.0/queryable-countries
```

#### S1.0a4 - OpenAPI Pages

Each OpenAPI document should be served as an interactive documentation page in development and production. Requests to OpenAPI document endpoints should not be authenticated.

#### S1.0a5 - Rate Limiting

The system should use simple rate limiting.

## Features out of scope

The system **does not** implement logging or caching.
