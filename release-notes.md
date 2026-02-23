# Changelog / release notes

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/). To see an example from a mature product in the program [see the Complete products changelog that follows the same methodology](https://github.com/DFE-Digital/complete-conversions-transfers-changes/blob/main/CHANGELOG.md).

## [Unreleased](https://github.com/DFE-Digital/Dfe.Academies.External/compare/production-2026-02-23.663...HEAD)

---
## [1.29.1][1.29.1] - 2026-02-23

### Change
- 261530 - Update A2B accessibility statement link

## [1.29.0][1.29.0] - 2026-01-21

### Fixed
- 250898 - tightened up validation for new FAM trust name and only send email in prod

---

## [1.28.0][1.28.0] - 2026-01-20

### Removed
- 257690 - Revert logging changes for P2 issue

---

## [1.27.0][1.27.0] - 2026-01-20

### Added
- 257690 - Added logging to help investigate P2 issue

### Security
- updated key vault version to 0.5.2

---

## [1.26.0][1.26.0] - 2025-12-19

### Fixed
- 251167 - Fix Gap filler - A2B incorrect Text font

---

## [1.25.0][1.25.0] - 2025-12-08

### Fixed
- 249941 - Improve search functionality

---

## [1.24.0][1.24.0] - 2025-12-04

### Fixed
- 188869 - Handled 403 Forbidden SharePoint Error message and display max upload

---

## [1.23.0][1.23.0] - 2025-11-18

### Fixed
- 240911 - Add HttpOnly = true to cookies

### Security
- update pin dependencies 

---

## [1.22.0][1.22.0] - 2025-07-11

### Changed
- 213024 - a2b gov uk rebrand

### Fixed
- 213024 - a2b gov uk rebrand capitalise Beta

### Security
- renovate dependency updates
- Updated renovate config to use the shared config - Added package validation workflow step

---

## [1.21.0][1.21.0] - 2024-12-17

### Fixed
- 191269 - Removed banner after submitting the app

---

## [1.20.0][1.20.0] - 2024-12-16

### Fixed
- Switch name to match ASPNETCORE_ENVIRONMENT

---

## [1.19.0][1.19.0] - 2024-11-29

### Added
- Add MIT Licence in accordance to DfE policy

### Security
- renovate dependency updates

---

## [1.18.0][1.18.0] - 2024-11-26

### Changed
- 189553 - update accessibility statement

### Fixed
- 188999 - Fixed accessibility issues for both adding trust and school pages
- 190186 - fixed wrong text link

---

## [1.17.0][1.17.0] - 2024-11-25

### Changed
- Switch to Azure Linux base image

### Security
- renovate dependency updates

---

## [1.16.0][1.16.0] - 2024-11-20

### Changed 
- 187235 - No banner display if app is submitted
- 186437 - Validate all FE points are protected with anonymous FE endpoints

### Security
- renovate dependency updates

---

## [1.15.0][1.15.0] - 2024-09-24

### Changed
- 176676 - Changed Grant Name from Pre-opening to Conversion

### Security
- renovate dependency updates

---

## [1.14.0][1.14.0] - 2024-08-01

---

## [1.13.0][1.13.0] - 2024-04-18

---

## [1.12.0][1.12.0] - 2024-04-18

---

## [1.11.0][1.11.0] - 2024-04-15

---

## [1.10.0][1.10.0] - 2024-03-19

---

## [1.9.0][1.9.0] - 2024-02-08

---

## [1.8.0][1.8.0] - 2024-01-03

---

## [1.7.0][1.7.0] - 2023-12-18

---

## [1.6.0][1.6.0] - 2023-08-08

---

## [1.5.0][1.5.0] - 2023-08-07

---

## [1.4.0][1.4.0] - 2023-08-02

---

## [1.3.0][1.3.0] - 2023-05-18

### Changed
- 127457 - default radio buttons trust journey

### Removed
- 123780 - phone numbers removed from privacy

### Fixed
- 124884 - radio button defaults school
- 129162 - bug fix radio button defaults change school name

---

## [1.2.0][1.2.0] - 2023-05-04

### Changed
- add option to toggle off container health probe
- Specifying the number of maximum replicas allows for easy scaling up/down.
- Specify a Host Header option for the CDN origin
- 127535 - change you answers prevented after submit
- 127679 - text boxes increase character limit
- 127793 - negative value in leases and also loans

### Fixed
- 122310 - validation rule fixes
- 127618 - remove job to fix sharepoint folders
- redis fix
- 127679 - cosmetic fix to the trust key people page

---

## [1.1.0][1.1.0] - 2023-04-18

### Changed
- 120230 - changed terms content page
- Changed the path that files are expected to be for schools

### Fixed
- 122901 - Changed status calculation

---

## [1.0.0][1.0.0] - 2023-02-23

- initial production release


[1.29.1]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2026-02-23.663
[1.29.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2026-02-10.657
[1.28.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2026-01-20.643
[1.27.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2026-01-20.641
[1.26.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2025-12-19.637
[1.25.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2025-12-08.633
[1.24.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2025-12-04.630
[1.23.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2025-11-18.626
[1.22.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2025-07-11.615
[1.21.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-12-17.540
[1.20.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-12-16.534
[1.19.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-11-29.514
[1.18.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-11-26.494
[1.17.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-11-25.487
[1.16.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-11-20.476
[1.15.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-09-24.431
[1.14.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-08-01.402
[1.13.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-04-18.284
[1.12.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-04-18.283
[1.11.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-04-15.271
[1.10.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-03-19.226
[1.9.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-02-08.208
[1.8.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2024-01-03.202
[1.7.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2023-12-18.201
[1.6.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2023-08-08.149
[1.5.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2023-08-07.138
[1.4.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2023-08-02.129
[1.3.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2023-05-18.85
[1.2.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2023-05-04.73
[1.1.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/production-2023-04-18.57
[1.0.0]: https://github.com/DFE-Digital/Dfe.Academies.External/releases/tag/release-1
