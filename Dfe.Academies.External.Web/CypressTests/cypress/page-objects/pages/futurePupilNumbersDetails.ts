class FuturePupilNumbersDetails {
  public enterFuturePupilNumbersDetails(): this {
    cy.get('#ProjectedPupilNumbersYear1').type('999')
    cy.get('#ProjectedPupilNumbersYear2').type('1499')
    cy.get('#ProjectedPupilNumbersYear3').type('1999')
    cy.get('#SchoolCapacityAssumptions').type('School Capacity Assumptions')
    cy.get('#SchoolCapacityPublishedAdmissionsNumber').type('999')

    cy.get('input[type=submit]').click()

    return this
  }
}

const futurePupilNumbersDetails = new FuturePupilNumbersDetails()

export default futurePupilNumbersDetails
