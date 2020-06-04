import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { 
      blockHeight: 0,
      address: "",
      transactions: [],
      blockHash: "",
      blockNumber: 0
     };

     this.handleInputChange = this.handleInputChange.bind(this);
     this.populateTransactionData = this.populateTransactionData.bind(this);
  }

  handleInputChange(event) {
    const target = event.target;
    const value = target.value;
    const name = target.name;

    this.setState({
        [name]: value
    });
  }

  async populateTransactionData() {
    const response = await fetch('ethereum/search?blockHeight=' + this.state.blockHeight + '&address=' + this.state.address);
    const data = await response.json();
    console.log(data);
    this.setState({ 
      transactions: data.transactions,
      blockNumber: data.blockNumber,
      blockHash: data.blockHash
     });
  }

  render () {

    return (
      <div>
        <h1>Hello, BTC Markets!</h1>
        <p>Try putting a height in below. Optionally add an address to filter within a block.</p>
        <div>
          <div>
            <label>
            Block Height: 
            <input className="form-control" type="number" name="blockHeight" value={this.state.blockHeight} onChange={this.handleInputChange} />
            </label>
          </div>
          <div>
            <label>
            Address:
            <input className="form-control" type="text" name="address" value={this.state.address} onChange={this.handleInputChange} />
            </label>
          </div>
          <div>
            <button className="btn-primary" onClick={this.populateTransactionData}>
              Search
            </button>
          </div>

        </div>
        {this.state.blockHash === ""
        ?  <div></div>
        : <div>
            <p>Results for block {this.state.blockNumber}-{this.state.blockHash}</p>
            <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
              <tr>
                <th>Tx Hash</th>
                <th>Gas</th>
                <th>From -> To</th>
                <th>Value</th>
              </tr>
            </thead>
            <tbody>
              {this.state.transactions.map(transaction =>
                <tr key={transaction.txHash}>
                  <td>{transaction.txHash}</td>
                  <td>{transaction.gas}</td>
                  <td>
                    <div>{transaction.from}</div>
                    <div>{transaction.to}</div>
                  </td>
                  <td>{transaction.value} Eth</td>
                </tr>
              )}
            </tbody>
          </table> 
          </div>
        }
      </div>
    );
  }

}
