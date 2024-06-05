class FuturePupilNumbersDetails {
  public futurePupilNumbersDetailsElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Future pupil numbers')

    cy.get('label[for=ProjectedPupilNumbersYear1]').contains('Projected pupil numbers on roll in the year the academy opens (year 1)')
    cy.get('#ProjectedPupilNumbersYear1').should('be.enabled')

    cy.get('label[for=ProjectedPupilNumbersYear2]').contains('Projected pupil numbers on roll in the following year after the academy has opened (year 2)')
    cy.get('#ProjectedPupilNumbersYear2').should('be.enabled')

    cy.get('label[for=ProjectedPupilNumbersYear3]').contains('Projected pupil numbers on roll in the following year (year 3)')
    cy.get('#ProjectedPupilNumbersYear3').should('be.enabled')

    cy.get('label[for=SchoolCapacityAssumptions]').contains('What do you base these projected numbers on?')
    cy.get('#SchoolCapacityAssumptions').should('be.enabled')

    cy.get('label[for=SchoolCapacityPublishedAdmissionsNumber]').contains('What is the school\'s published admissions number (PAN)?')
    cy.get('#SchoolCapacityPublishedAdmissionsNumber').should('be.enabled')

    cy.get('input[type=submit]').should('be.visible').contains('Save and return to overview')

    return this
  }

  public fillFuturePupilNumbersDetails(): this {
    cy.get('#ProjectedPupilNumbersYear1').type('999')
    cy.get('#ProjectedPupilNumbersYear2').type('1499')
    cy.get('#ProjectedPupilNumbersYear3').type('1999')
    cy.get('#SchoolCapacityAssumptions').type('School Capacity Assumptions')
    cy.get('#SchoolCapacityPublishedAdmissionsNumber').type('999')

    return this
  }

  public submitFuturePupilNumbersDetails(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const futurePupilNumbersDetails = new FuturePupilNumbersDetails()

export default futurePupilNumbersDetails
