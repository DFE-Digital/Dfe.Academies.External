class LandAndBuildingsDetails {
  public landAndBuildingsDetailsElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Land and buildings')

    cy.get('label[for=SchoolBuildLandOwnerExplained]').contains('As far as you\'re aware, who owns or holds the school\'s buildings and land?')
    cy.get('#SchoolBuildLandOwnerExplained').should('be.enabled')

    cy.get('legend').eq(0).contains('Are there any current or planned building works?')

    cy.get('#buildingWorksOptionYes').should('not.be.checked')
    cy.get('label[for=buildingWorksOptionYes]').contains('Yes')

    cy.get('#buildingWorksOptionNo').should('not.be.checked')
    cy.get('label[for=buildingWorksOptionNo]').contains('No')

    cy.get('legend').eq(1).contains('Are there any shared facilities on site?')

    cy.get('span[class=govuk-hint govuk-body govuk-!-margin-bottom-5]').contains('For example, a nursery, children’s centre, swimming pool, leisure centre, caretaker’s house, community library or SEN unit')

    cy.get('#sharedFacilitiesOptionYes').should('not.be.checked')
    cy.get('label[for=sharedFacilitiesOptionYes]').contains('Yes')

    cy.get('#sharedFacilitiesOptionNo').should('not.be.checked')
    cy.get('label[for=sharedFacilitiesOptionNo]').contains('No')

    cy.get('legend').eq(2).contains('Has the school had any grants from Sport England, the Big Lottery Fund, or the Football Federation?')

    cy.get('#landGrantsOptionYes').should('not.be.checked')
    cy.get('label[for=landGrantsOptionYes]').contains('Yes')

    cy.get('#landGrantsOptionNo').should('not.be.checked')
    cy.get('label[for=landGrantsOptionNo]').contains('No')

    cy.get('legend').eq(3).contains('Is the school part of a Private Finance Initiative (PFI) scheme?')

    cy.get('#pfiSchemeOptionYes').should('not.be.checked')
    cy.get('label[for=pfiSchemeOptionYes]').contains('Yes')

    cy.get('#pfiSchemeOptionNo').should('not.be.checked')
    cy.get('label[for=pfiSchemeOptionNo]').contains('No')

    cy.get('legend').eq(4).contains('Is the school part of the Priority School Building Programme?')

    cy.get('#SchoolBuildLandPriorityBuildingProgrammeYes').should('not.be.checked')
    cy.get('label[for=SchoolBuildLandPriorityBuildingProgrammeYes]').contains('Yes')

    cy.get('#SchoolBuildLandPriorityBuildingProgrammeNo').should('not.be.checked')
    cy.get('label[for=SchoolBuildLandPriorityBuildingProgrammeNo]').contains('No')

    cy.get('legend').eq(5).contains('Is the school part of the Building Schools for the Future Programme?')

    cy.get('#SchoolBuildLandFutureProgrammeYes').should('not.be.checked')
    cy.get('label[for=SchoolBuildLandFutureProgrammeYes]').contains('Yes')

    cy.get('#SchoolBuildLandFutureProgrammeNo').should('not.be.checked')
    cy.get('label[for=SchoolBuildLandFutureProgrammeNo]').contains('No')

    cy.get('input[type=submit]').should('be.visible').contains('Save and return to overview')

    return this
  }

  public fillLandAndBuildingsDetailsDataAndSubmit(): this {
    const landOwnerExplained = 'As far as you\'re aware, who owns or holds the school\'s buildings and land?'
    cy.get('#SchoolBuildLandOwnerExplained').type(landOwnerExplained)

    cy.get('#buildingWorksOptionNo').click()
    cy.get('#buildingWorksOptionNo').should('be.checked')

    cy.get('#sharedFacilitiesOptionNo').click()
    cy.get('#sharedFacilitiesOptionNo').should('be.checked')

    cy.get('#landGrantsOptionNo').click()
    cy.get('#landGrantsOptionNo').should('be.checked')

    cy.get('#pfiSchemeOptionNo').click()
    cy.get('#pfiSchemeOptionNo').should('be.checked')

    cy.get('#SchoolBuildLandPriorityBuildingProgrammeNo').click()
    cy.get('#SchoolBuildLandPriorityBuildingProgrammeNo').should('be.checked')

    cy.get('#SchoolBuildLandFutureProgrammeNo').click()
    cy.get('#SchoolBuildLandFutureProgrammeNo').should('be.checked')

    cy.get('input[type=submit]').click()

    return this
  }
}

const landAndBuildingsDetails = new LandAndBuildingsDetails()

export default landAndBuildingsDetails
