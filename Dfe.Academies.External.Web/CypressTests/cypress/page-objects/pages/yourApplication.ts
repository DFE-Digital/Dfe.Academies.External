class YourApplication {
  public yourApplicationNotStartedElementsVisible(): this {
    cy.yourApplicationNotStartedElementsVisible()

    return this
  }

  public yourApplicationNotStartedButSchoolAddedElementsVisible(): this {
    cy.yourApplicationNotStartedButSchoolAddedElementsVisible()

    return this
  }

  public yourApplicationNotStartedButTrustSectionCompleteElementsVisible(): this {
    cy.yourApplicationNotStartedButTrustSectionCompleteElementsVisible()

    return this
  }

  public selectAddATrust(): this {
    cy.selectAddATrust()

    return this
  }

  public selectAddASchool(): this {
    cy.selectAddASchool()

    return this
  }

  public selectTrustDetails(): this {
    cy.selectTrustDetails()

    return this
  }

  public selectChangeSchool(): this {
    cy.selectChangeSchool()

    return this
  }

  public selectChangeTrust(): this {
    cy.selectChangeTrust()

    return this
  }

  public selectAboutTheConversion(): this {
    cy.contains('About the conversion').click()

    return this
  }

  public yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible(): this {
    cy.yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible()

    return this
  }

  public selectFurtherInformation(): this {
    cy.contains('Further information').click()

    return this
  }

  public yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible(): this {
    cy.yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible()

    return this
  }

  public selectFinances(): this {
    cy.contains('Finances').click()

    return this
  }

  public financeCompleteElementsVisible(): this {
    cy.financeCompleteElementsVisible()

    return this
  }

  public selectFuturePupilNumbers(): this {
    cy.selectFuturePupilNumbers()

    return this
  }

  public futurePupilNumbersCompleteElementsVisible(): this {
    cy.futurePupilNumbersCompleteElementsVisible()

    return this
  }

  public selectLandAndBuildings(): this {
    cy.selectLandAndBuildings()

    return this
  }

  public landAndBuildingsCompleteElementsVisible(): this {
    cy.landAndBuildingsCompleteElementsVisible()

    return this
  }

  public selectConsultation(): this {
    cy.selectConsultation()

    return this
  }

  public consultationCompleteElementsVisible(): this {
    cy.consultationCompleteElementsVisible()

    return this
  }

  public selectPreopeningSupportGrant(): this {
    cy.selectPreopeningSupportGrant()

    return this
  }

  public preopeningSupportGrantCompleteElementsVisible(): this {
    cy.preopeningSupportGrantCompleteElementsVisible()

    return this
  }

  public selectDeclaration(): this {
    cy.contains('Declaration').click()

    return this
  }

  public declarationCompleteElementsVisible(): this {
    cy.declarationCompleteElementsVisible()

    return this
  }

  public submitApplication(): this {
    cy.get('input[type=submit]').click()

    return this
  }

  public selectCancelApplication(): this {
    cy.contains('Cancel application').click()

    return this
  }

  public selectInviteContributorLink(): this {
    cy.contains('invite or remove').click()

    return this
  }

  // Form A MAT specific
  public FAMApplicationNotStartedElementsVisible(): this {
    cy.FAMApplicationNotStartedElementsVisible()

    return this
  }

  public FAMApplicationNotStartedSchoolAddedElementsVisible(): this {
    cy.FAMApplicationNotStartedSchoolAddedElementsVisible()

    return this
  }

  public FAMApplicationSchoolCompleteElementsVisible(): this {
    cy.FAMApplicationSchoolCompleteElementsVisible()

    return this
  }

  public FAMApplicationTrustNameComplete(): this {
    cy.FAMApplicationTrustNameComplete()

    return this
  }

  public FAMApplicationFuturePupilNumbersSubmittedElementsVisible(): this {
    cy.FAMApplicationFuturePupilNumbersSubmittedElementsVisible()

    return this
  }

  public selectFAMSchool(): this {
    cy.selectFAMSchool()

    return this
  }

  public selectFAMAddTheTrust(): this {
    cy.selectFAMAddTheTrust()

    return this
  }

  public selectFAMTrustDetails(): this {
    cy.selectFAMTrustDetails()

    return this
  }

  public FAMApplicationOverviewCompleteElementsVisible(): this {
    cy.FAMApplicationOverviewCompleteElementsVisible()

    return this
  }
}

const yourApplication = new YourApplication()

export default yourApplication
