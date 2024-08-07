class LandAndBuildingsDetails {
  public enterLandAndBuildingsDetailsDetails(): this {
    cy.get('#SchoolBuildLandOwnerExplained').type('Land owners')

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
