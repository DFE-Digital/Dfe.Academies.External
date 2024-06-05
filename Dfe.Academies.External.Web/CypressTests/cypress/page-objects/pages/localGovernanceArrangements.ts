class LocalGovernanceArrangements {
  public localGovernanceArrangementsElementsVisible(): this {
    cy.get('a[class=govuk-back-link]').contains('Back')
    cy.get('.govuk-caption-l').eq(0).contains('PLYMOUTH CAST (step 3 of 3)')
    cy.get('.govuk-heading-l').contains('Local governance arrangements')
    cy.get('legend').eq(0).contains('Will there be any changes at a local level due to this school joining?')
    cy.get('.govuk-caption-l').eq(1).contains('For example, setting up a local sub-regional hub or changes to any schemes of delegation.')
    cy.get('#changesToLaGovernanceYes').should('not.be.checked')
    cy.get('label[for=changesToLaGovernanceYes]').contains('Yes')
    cy.get('#changesToLaGovernanceNo').should('not.be.checked')
    cy.get('label[for=changesToLaGovernanceNo]').contains('No')
    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

    return this
  }

  public localGovernanceArrangementsClickYes(): this {
    cy.get('#changesToLaGovernanceYes').click()

    return this
  }

  public enterlocalGovernanceArrangementsChanges(): this {
    const localGovernanceArrangements = 'What are the changes and how do they fit into the current lines of accountability in the trust? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et d'
    cy.get('#changesToLaGovernanceYes').click()
    cy.get('#ChangesToLaGovernanceExplained').click()
    cy.get('#ChangesToLaGovernanceExplained').type(localGovernanceArrangements)

    return this
  }

  public localGovernanceArrangementsSubmit(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const localGovernanceArrangements = new LocalGovernanceArrangements()

export default localGovernanceArrangements
