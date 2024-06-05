class ChangesToTheTrust {
  public changesToTheTrustElementsVisible(): this {
    cy.get('a[class=govuk-back-link]').contains('Back')
    cy.get('.govuk-caption-l').eq(0).contains('PLYMOUTH CAST (step 2 of 3)')
    cy.get('.govuk-heading-l').contains('Changes to the trust')
    cy.get('legend').eq(0).contains('Will there be changes to the governance of the trust due to the school joining?')
    cy.get('.govuk-caption-l').eq(1).contains('For example, changes to the trustees or their roles')
    cy.get('#revenueTypeYes').should('not.be.checked')
    cy.get('.govuk-label').eq(0).contains('Yes')
    cy.get('#revenueTypeNo').should('not.be.checked')
    cy.get('.govuk-label').eq(2).contains('No')
    cy.get('#revenueTypeUnknown').should('not.be.checked')
    cy.get('.govuk-label').eq(3).contains('Unknown at this point')
    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

    return this
  }

  public changesToTheTrustClickYesEnterChangesAndSubmit(): this {
    cy.get('#revenueTypeYes').click()

    const changesToTheTrust = 'What are the changes? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ull'
    cy.get('#PFYRevenueStatusExplained').click()
    cy.get('#PFYRevenueStatusExplained').type(changesToTheTrust)

    cy.get('input[type=submit]').click()

    return this
  }
}

const changesToTheTrust = new ChangesToTheTrust()

export default changesToTheTrust
